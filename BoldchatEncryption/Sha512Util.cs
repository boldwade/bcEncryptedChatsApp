using System;
using System.Security.Cryptography;
using System.Text;

namespace BoldchatEncryption
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

        private static string BytesToHex(byte[] bytes)
        {
            var hexChars = new char[bytes.Length * 2];
            for (var j = 0; j < bytes.Length; j++)
            {
                var v = bytes[j] & 0xFF;
                hexChars[j * 2] = HexArray[v >> 4];
                hexChars[j * 2 + 1] = HexArray[v & 0x0F];
            }
            return new string(hexChars);
        }

        //private static string EncodeUrl(string url)
        //{
        //    var encodedUrl = HttpUtility.UrlEncode(url, Encoding.UTF8);
        //    return UpperCaseEncodedUrl(encodedUrl);
        //}

        //private static string UpperCaseEncodedUrl(string encodedUrl)
        //{
        //    var temp = encodedUrl.ToCharArray();
        //    for (var i = 0; i < temp.Length - 2; i++)
        //    {
        //        if (temp[i] == '%')
        //        {
        //            temp[i + 1] = char.ToUpper(temp[i + 1]);
        //            temp[i + 2] = char.ToUpper(temp[i + 2]);
        //        }
        //    }
        //    return new string(temp);
        //}
    }
}