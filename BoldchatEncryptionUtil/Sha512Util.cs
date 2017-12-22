using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BoldchatEncryptionUtil
{
    public class Sha512Util
    {
        private static readonly char[] HexArray = "0123456789ABCDEF".ToCharArray();
        private readonly string _hashKey;

        public Sha512Util(string hashKey)
        {
            if (string.IsNullOrEmpty(hashKey))
            {
                throw new Exception("You must include a hashKey to use this class");
            }

            _hashKey = hashKey;
        }

        public string GetEncryptedString(string input)
        {
            var hashBytes = new HMACSHA512(Encoding.UTF8.GetBytes(_hashKey)).ComputeHash(Encoding.UTF8.GetBytes(input));
            var hashText = BytesToHex(hashBytes);
            return hashText + input;
        }

        private static string BytesToHex(IReadOnlyList<byte> bytes)
        {
            var hexChars = new char[bytes.Count * 2];
            for (var j = 0; j < bytes.Count; j++)
            {
                var v = bytes[j] & 0xFF;
                hexChars[j * 2] = HexArray[v >> 4];
                hexChars[j * 2 + 1] = HexArray[v & 0x0F];
            }
            return new string(hexChars);
        }
    }
}