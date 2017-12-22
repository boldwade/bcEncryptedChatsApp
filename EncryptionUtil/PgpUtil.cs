using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;

namespace EncryptionUtil {
    public class PgpUtil {

        private readonly byte[] _encryptionKey;
        private readonly byte[] _signingKey;
        private readonly char[] _password;

        public PgpUtil(byte[] encryptionKey, byte[] signingKey, char[] password) {
            if (encryptionKey == null || encryptionKey.Length == 0) {
                throw new Exception("You must include a valid encoded encryptionKey: Encoding.UTF8.GetBytes");
            }
            if (signingKey == null || signingKey.Length == 0) {
                throw new Exception("You must include a valid encoded signingKey: Encoding.UTF8.GetBytes");
            }
            if (password == null || password.Length == 0) {
                throw new Exception("You must include a valid password");
            }

            _encryptionKey = encryptionKey;
            _signingKey = signingKey;
            _password = password;
        }

        public string GetEncryptedString(string input)
        {
            var byteInput = Encoding.UTF8.GetBytes(input);
            var encryptedData = GetEncryptedData(byteInput);
            return Encoding.UTF8.GetString(encryptedData);
        }

        private byte[] GetEncryptedData(byte[] data) {
            var baos = new MemoryStream();
            var outStr = new ArmoredOutputStream(baos);

            PgpPublicKey publicKey = null;
            var inputStream = PgpUtilities.GetDecoderStream(new MemoryStream(_encryptionKey));
            var pgpPub = new PgpPublicKeyRingBundle(inputStream);
            for (var i = pgpPub.GetKeyRings().GetEnumerator(); i.MoveNext();) {
                var pgpPublicKeyRing = (PgpPublicKeyRing)i.Current;
                if (pgpPublicKeyRing != null)
                    for (var j = pgpPublicKeyRing.GetPublicKeys().GetEnumerator();
                        publicKey == null && j.MoveNext();) {
                        var k = (PgpPublicKey)j.Current;
                        if (k != null && k.IsEncryptionKey) {
                            publicKey = k;
                        }
                    }
            }
            if (publicKey == null) throw new Exception("Can't find encryption key in key ring.");

            var pgpSec = new PgpSecretKeyRingBundle(PgpUtilities.GetDecoderStream(new MemoryStream(_signingKey)));
            PgpPrivateKey privateKey = null;
            PgpSecretKey secretKey = null;
            for (var i = pgpSec.GetKeyRings().GetEnumerator(); privateKey == null && i.MoveNext();) {
                var keyRing = (PgpSecretKeyRing)i.Current;
                if (keyRing != null)
                    for (var j = keyRing.GetSecretKeys().GetEnumerator(); j.MoveNext();) {
                        secretKey = (PgpSecretKey)j.Current;
                        if (secretKey != null) privateKey = secretKey.ExtractPrivateKey(_password);
                        break;
                    }
            }
            if (secretKey == null) throw new Exception("Can't find signature key in key ring.");
            var cb = new MemoryStream();
            var compressedGenerator = new PgpCompressedDataGenerator(CompressionAlgorithmTag.Zip);
            var compressedOut = compressedGenerator.Open(cb);
            var signatureGenerator = new PgpSignatureGenerator(secretKey.PublicKey.Algorithm,
                HashAlgorithmTag.Sha512);
            signatureGenerator.InitSign(PgpSignature.BinaryDocument, privateKey);
            for (var i = secretKey.PublicKey.GetUserIds().GetEnumerator(); i.MoveNext();) {
                var spGen = new PgpSignatureSubpacketGenerator();
                spGen.SetSignerUserId(false, (String)i.Current);
                signatureGenerator.SetHashedSubpackets(spGen.Generate());
            }
            signatureGenerator.GenerateOnePassVersion(true).Encode(compressedOut);
            var lgen = new PgpLiteralDataGenerator();
            var finalOut = lgen.Open(compressedOut, PgpLiteralData.Binary, "", DateTime.Now, new byte[4096]);
            finalOut.Write(data, 0, data.Length);
            signatureGenerator.Update(data);
            finalOut.Close();
            lgen.Close();
            signatureGenerator.Generate().Encode(compressedOut);
            compressedGenerator.Close();
            compressedOut.Close();
            var compressedData = cb.ToArray();
            var encryptedDataGenerator =
                new PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag.Aes256, true,
                    new SecureRandom());
            encryptedDataGenerator.AddMethod(publicKey);
            var encryptedOut = encryptedDataGenerator.Open(outStr, compressedData.Length);
            encryptedOut.Write(compressedData, 0, compressedData.Length);
            encryptedOut.Close();
            encryptedDataGenerator.Close();
            outStr.Close();
            return baos.ToArray();
        }

    }
}
