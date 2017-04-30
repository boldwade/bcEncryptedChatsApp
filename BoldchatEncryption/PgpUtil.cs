using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;

namespace BoldchatEncryption {
    public class PgpUtil {

        private readonly byte[] _encryptionKey;
        private readonly byte[] _signingKey;
        private readonly char[] _password;

        //public PgpUtil() : this(Encoding.UTF8.GetBytes(
        //        "-----BEGIN PGP PUBLIC KEY BLOCK-----\r\nVersion: BCPG v1.50\r\n\r\nmQENBFV1yZgDCACuELcZQ6MpqjLbMmF+ZlbsvVcLM98iRN90pPvC46LlZtic0IZ6\r\ntg854rOpGTkiQkkf4PBvMp8K04ImhxpveTass7zHeJ8BGhlUPeVZ63QkF4m7pipo\r\nDWkilgJv4ALC53MHQ1VM2G90XUYrSzQzOgxMOeNyx6zcILW9qSplsnVD/8iVizxw\r\nlgtbuDFE4zt6EkiPhssXQuS0VWB+5GveEFUoAzG2sc1ZwZHlJ43C4vqG//+68xFP\r\nlprCUmdk4Irwy+aR8RYqgFXzYYAtNTYnJTby2l0OmNLHdsKmi+rxIPIs6ZwFfRJn\r\ngQNZbv0pCvJQN9zVLkBruNpmD9SxbuOkKCNvABEBAAG0CGJvbGRjaGF0iQE3BBAD\r\nCgAhBQJVdcmZAhsDBAsJCAcGFQoIAgkLBAsJCAcGFQoIAgkLAAoJEDxFaLDmEsVD\r\nB7oIAIz5OMM3hnNBybWXThWxkxaNPOqNWxFb1z35BkLeeTMU1u9m4yk/NmbrVrBO\r\nI1olrUJrWH43cE2aMfyeSi4KWsfc6J0oZggMM2e5LRiaw2WxPb8e2VU7VhkWfBk6\r\nQ4vXyOuWkn78NcYPZrBJlcSwY7tF7ib5nmoEvoTAZkuQMOJ0sLE8bsJ1lNY8V3KF\r\nEV9CDizk37nF0+2TiAMm5gogtCw0RMGvEwlbl+aVQ6voJBKp5FqQWcfyhxKgytiX\r\nuBlJbdoPhiceSYq/7+ynWM1y7XR0by61lxyBSRGL9tj8bHxnUP9Aw8zuGHqbt820\r\n3+kf273pqKTKs4WfhHprw4eiPDu5AQ0EVXXJmQEIAJUPFod65jlH38ChqXCnWYoN\r\nwGugI5IQgtsacz6x+2QlOMDENXcbpGYKDEBFh5VtA1Eh4bHtXJI3ZHysVZXAjmqo\r\n4VXF5p6FCCgqGe9fIYre9UOWEyDTFIR+SJh+8k13YQogYcpurqpmywaPvsdSWGuL\r\nI5qong5LfVmAKgoopj0YR+mdjQvh24XOYK+OwMMUiehlYgMfgbqv53RJ66ImiVEO\r\nQ3unuzmtmqOA+K4NoKtkpC0nmZkYDErthADhJvuAy+NP3XTh+L7jw2/cGqPCCSK3\r\n8W5NO4xJuYKQcBcW63XnfYy1S2d917jwrbHUeJCmM/Sg5CPgRMAO/cpPkASShwEA\r\nEQEAAYkBHwQYAwoACQUCVXXJmQIbDAAKCRA8RWiw5hLFQ04ECACR0XIoXHbcUz+1\r\nbcmnqi/+ALkvk/R3M2MOTrtabm7e0SU34RJRdSBtXguwbCoMYZq5XudBxh/lyKT6\r\nrB4iTJ96DRVrPnK/rDZjyrtoYAKl7hmgKsh+KvOLwWRNcvfkuo0Kj60Eajl9bdrG\r\nNIOZtnb6FfNC3gHlI2yiuq5VhVx9X8E/fqgR9Q6KGEEGgKrWynSPU9CuzrDD6Ihc\r\nD5cK+wR6hWwVNbXyIbmNzK1P1+/Fjfv6tBxKEDhHVHhKulO76ovzjx9ZElyWUHcz\r\nlSSeea+qDK3kiVzW6gIhgWtTDoje++ORFVW03I5ar1e83QMMK6JzqLC4ZGBepPt1\r\n+bfZFDv2\r\n=WTIz\r\n-----END PGP PUBLIC KEY BLOCK-----\r\n"), Encoding.UTF8.GetBytes(
        //            "-----BEGIN PGP PRIVATE KEY BLOCK-----\r\nVersion: BCPG C# v1.6.1.0\r\n\r\nlQOsBFV1ydkBCACSPbHF300qyX9BVdNG37vvuXuLH5rlzY5S4AFom+pZuCQmIpBx\r\n9Uzvo9bq/2+DQs454gnNwRiIQXBWeVBl/UO8BQSqh9ej9QcQrG6IJavluiIqupKl\r\n1b4HfbNJAhUSLFbNZsrKNq+0krrsjopxGuaFF9uC7bDGYygLZ7h25FGs/eOFLhbH\r\nOx3tmqAceaPuAa171C2dp/mVw2UIswEg4eF5JSWAqVNOHpRXCRe+uNt0Kw4+Lb10\r\nr5PdGu+cYingXh5U5IS+bXwfCX4soWmMSmeyQ7jcTdvAih/jZLTbLKaE5sM1ApCr\r\nZ650g63ilDm05gCwkWHVT41IhfqAFlPose4/ABEBAAH/AwMC+zEjzEZbq9tgfYCx\r\nmYyLbQhLCoz2PLSyTLx2lLWXd9mwelgAzmDqvhu7rZJ2cjb9zA/MxJ55axbzI3zC\r\n3c04AvCmuLv12cjM+p6jIHlp+Be0uTCxvM+cHiho+Y4SOog79IIcVpXkxi78og32\r\n6P7zzYzp3NYahDHu+rU3eRbxoHQY8L5V4g1f3gTmg4ettAOLyxI255dFobEV0S3q\r\nt+y4/Y7RCIJKpox542jqn1ov2Z59PqGSzWyPFzWdi2bkYiq2StTC9Xdt0g4MuYwF\r\nWs/b9KU04Bh/VujHPmc754lFYwxtdMGZULHEIB1RQ4DKLET+eBmQeebcVxeK6Vjx\r\nris2xFJvW+cBJStJaUXkDhcrlimQZsvdtf50Zs2ByGM+NFkeiRGrAMRL1jMdFdPa\r\nRolf1VyxPJpligYqIUY+cf2h9H5+rNZXv6A/Vc34xqWrjMrRzihBt18kjact15TA\r\nPm/d+PSoq+VBbi2HsrBhrfSM8ZCN2uSIL2Kcw93snq62lAq7X0hkeVrWWbrp+sut\r\n3dDybCDbPEPzU6ULvyyeoa9QDK7y2DKQZezt7UUDtjyfI347WXjD7BVohDDXZa3B\r\nWf8/sJatLXHaHpvKdTOg8qj1XDVA6suSyjugVcySOT9San42zRUz3REoVTGpJWZQ\r\nNkpxdaHA121TM/5/WEbXcpielvk6Cfd/NeJTy3InM4mTOlCqCmKbNXqW+HfDzsTo\r\nM9Xs+qqey7yXk9Rvz6ARhNnFyFc42oz4K+VSOBtFZ7Lf8tI4yQn6ry73+90dx+zV\r\n09PZ49vqcS7Qt5+cTpAZrxO2cyZPTYZnvMwGLItmp3YgIngfXhorOLh/i2++h75Y\r\njabcCuaA7/uSE+hJB/Qg9y79jPxwbVu8deCKkiCt0LQAiQEcBBABAgAGBQJVdcnZ\r\nAAoJEAYhq9JZQy2K/q4IAIWjC4gSVHdDkKPpUP+MhS0UuoWfbUEkrweAXoUeyc81\r\nakqnhs0zy46UpURUH6/kS9X6O09DpAPnN5Cjfzwlz4CMXnm6GePNUQwkUd42K9MV\r\nWmsGM5LgV6lDv87dqb2mwcZTaZmj+8VZq5PhyPzaN1DsAhm/EvOhikBwvfuPJJWM\r\nZbCI1z2QrXzmE84zaHf58llzUe6wjBgnZqg+aenae875CGzWZc1C0zJNMp7q+p/b\r\nBcoM4dpx0/jjGdROUIgN5m8V41Yts42N3x0HEoYgpLpABvqfXiHE8MFyowWdFxGl\r\nVguSI7BVZ4MqKHNpNbnBLIn7PP1F6h3M7x9kHzMmLz8=\r\n=WF+n\r\n-----END PGP PRIVATE KEY BLOCK-----\r\n"), "boldchat".ToCharArray()) {
        //}

        //public PgpUtil(byte[] encryptionKey, byte[] signingKey) : this(encryptionKey, signingKey, "boldchat".ToCharArray()) {
        //}

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

        public string GetEncryptedString(string input) {
            return Encoding.UTF8.GetString(GetEncryptedData(Encoding.UTF8.GetBytes(input)));
        }

        private byte[] GetEncryptedData(byte[] data) {
            var baos = new MemoryStream();
            Stream o = new ArmoredOutputStream(baos);
            PgpPublicKey publicKey = null;
            var inputStream =
                PgpUtilities.GetDecoderStream(new MemoryStream(_encryptionKey));
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
            var pgpSec =
                new PgpSecretKeyRingBundle(
                    PgpUtilities.GetDecoderStream(new MemoryStream(_signingKey)));
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
            var encryptedOut = encryptedDataGenerator.Open(o, compressedData.Length);
            encryptedOut.Write(compressedData, 0, compressedData.Length);
            encryptedOut.Close();
            encryptedDataGenerator.Close();
            o.Close();
            return baos.ToArray();
        }

    }
}
