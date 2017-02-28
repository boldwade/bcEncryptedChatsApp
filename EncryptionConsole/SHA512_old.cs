//using System;
//using System.Collections;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Security.Cryptography;

//namespace Encryption
//{
//    class SHA512
//    {
//        static void Main(string[] args)
//        {
//            long accountID = 2307475884L;
//            long websiteID = 665123557559499L;
//            long chatButtonID = 58444531713309044L;
//            long floatingChatButtonID = 660128588754890L;

//            long expiration = (long)((DateTime.Now - DateTime.Parse("1/1/1970 0:0:0 GMT", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None).ToUniversalTime()).TotalMilliseconds + 30000);
//            string url = "http://boldchat.com";
//            string referrer = "http://google.com";
//            string chatKey = "Something unique to chat";
//            string visitorKey = "something unique to visitor";
//            string appendData = "&ChatKey=" + chatKey + "&VisitorKey=" + visitorKey + "&Expiration=" + expiration;
//            string customData = "&CustomURL=http://boldchat.com&VisitRef=a ref&VisitInfo=info&VisitEmail=email@gmail.com&VisitPhone=3166704000&URL="
//                + Uri.EscapeUriString(url) + "&ReferrerURL=" + Uri.EscapeUriString(referrer);

//            string chat = "ChatButtonDefID=" + chatButtonID + customData + appendData;
//            string invite = "FloatingChatButtonDefID=" + floatingChatButtonID + "&InvitationDefID=2076721744768832302&VisitName=floating" + customData + appendData;
//            string conversion = "ConversionCodeID=33221100&ConversionAmount=100.0&ConversionInfo=conversion info" + appendData;

//            string hashKey = "uWalJOJ8sOqcVuobDSwBIr7+KofZAq0K9/b0h/JviTYFqjpS/d0uIeewf/kgQLEM";

//            string hashedChat = appendHash(chat, hashKey);
//            string hashedInvite = appendHash(invite, hashKey);
//            string hashedConversion = appendHash(conversion, hashKey);
//            Console.WriteLine("Hashed Chat SecureParameters: " + hashedChat);
//            Console.WriteLine("Hashed Invite SecureParameters: " + hashedInvite);
//            Console.WriteLine("Hashed Conversion SecureParameters: " + hashedConversion);

//            Console.ReadLine();
//        }
//        public static String appendHash(String data, String hash)
//        {
//            byte[] hashBytes = SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes((hash + data)));
//            String hashText = bytesToHex(hashBytes);
//            return hashText + data;
//        }
//        protected static char[] hexArray = "0123456789ABCDEF".ToCharArray();
//        public static String bytesToHex(byte[] bytes)
//        {
//            char[] hexChars = new char[bytes.Length * 2];
//            for (int j = 0; j < bytes.Length; j++)
//            {
//                int v = bytes[j] & 0xFF;
//                hexChars[j * 2] = hexArray[v >> 4];
//                hexChars[j * 2 + 1] = hexArray[v & 0x0F];
//            }
//            return new String(hexChars);
//        }
//    }
//}