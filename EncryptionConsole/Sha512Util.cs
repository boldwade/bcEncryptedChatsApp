using System;

namespace Encryption {
    internal static class Sha512Util {
        //public static void Main(string[] args)
        //{
        //    var chatButtonID = 58444531713309044L;
        //    var floatingChatButtonID = 993383089899054;
        //    var expiration = (long)((DateTime.Now - DateTime.Parse("1/1/1970 0:0:0 GMT", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None).ToUniversalTime()).TotalMilliseconds + 30000);

        //    var url = "http://boldchat.com";
        //    var referrer = "http://google.com";

        //    var chatKey = "Something unique to chat " + expiration;
        //    var visitorKey = "something unique to visitor";
        //    var appendData = "&ChatKey=" + chatKey + "&VisitorKey=" + visitorKey + "&Expiration=" + expiration;
        //    var customData = "&CustomURL=http://boldchat.com&VisitRef=a ref&VisitInfo=info&VisitEmail=email@gmail.com&VisitPhone=3166704000&URL="
        //                        + EncodeUrl(url) + "&ReferrerURL=" + EncodeUrl(referrer);

        //    var chat = "ChatButtonDefID=" + chatButtonID + customData + appendData;
        //    var invite = "FloatingChatButtonDefID=" + floatingChatButtonID + "&InvitationDefID=2076721744768832302&VisitName=floating" + customData + appendData;
        //    var conversion = "ConversionCodeID=33221100&ConversionAmount=100.0&ConversionInfo=conversion info" + appendData;
        //    var hashKey = "uWalJOJ8sOqcVuobDSwBIr7+KofZAq0K9/b0h/JviTYFqjpS/d0uIeewf/kgQLEM";

        //    var hashedChat = AppendHash(chat, hashKey);
        //    var hashedInvite = AppendHash(invite, hashKey);
        //    var hashedConversion = AppendHash(conversion, hashKey);
        //    Console.WriteLine(String.Format("Hashed Chat SecureParameters: {0}\n", hashedChat));
        //    Console.WriteLine(String.Format("Hashed Invite SecureParameters: {0}\n", hashedInvite));
        //    Console.WriteLine(String.Format("Hashed Conversion SecureParameters: {0}\n", hashedConversion));

        //    Console.ReadLine();
        //}

        public static void Main(string[] args) {
            //const string hashKey_alphant2 = "yz55YHEQb8bBUDZXfuCZVYylvBSejyIS9CdoD8ISQKUZA/YxNg5n8FEsAwT/aE7u";
            //const string hashKey_dev = "KzFsK7RcNdJ094nb5NMJeitj6nac/lcCMdSj8QG/GorIJlqRmvyUJN0CIkufFjhI";
            const string hashKey_aid231 = "JU7wpBHyARkf/PofFxSgcmrmmX8U0+ykJXVmXCjKxayP8Pve1r5pB9mqzKAsyWbP";
            var shaUtil = new BoldchatEncryption.Sha512Util(hashKey_aid231);

            const string commonParms =                "VisitorKey=&Expiration=&URL=&ReferrerURL=&VisitRef=&VisitName=&VisitInfo=&VisitEmail=&VisitPhone=&InitialQuestion=&CustomURL=&customField_tag=&customField_text2=&customField_dropDownMenu=";

            var visitParms = "Type=visit&" + getParameters(commonParms, "visit");
            var floatingButtonParms = "Type=chat&ChatKey=&FloatingChatButtonID=2540228524023957271&" + getParameters(commonParms, "float");
            var staticButtonParms = "Type=chat&ChatKey=&ChatButtonID=8200206508610860737&" + getParameters(commonParms, "static");

            var hashedVisit = shaUtil.GetEncryptedString(visitParms);
            var hashedFloating = shaUtil.GetEncryptedString(floatingButtonParms);
            var hashedStatic = shaUtil.GetEncryptedString(staticButtonParms);

            Console.WriteLine("Hashed visit SecureParameters:");
            Console.WriteLine(hashedVisit);
            Console.WriteLine();

            Console.WriteLine("Hashed floating SecureParameters:");
            Console.WriteLine(hashedFloating);
            Console.WriteLine();

            Console.WriteLine("Hashed static SecureParameters:");
            Console.WriteLine(hashedStatic);
            Console.WriteLine();


            Console.ReadLine();
        }

        static string getParameters(string parms, string type)
        {
            return
                parms.Replace("VisitRef=", "VisitRef=myVisitRef_" + type)
                    .Replace("VisitName=", "VisitName=Jeffs Name_" + type)
                    .Replace("VisitInfo=", "VisitInfo=myVisitInfo_" + type)
                    .Replace("VisitEmail=", "VisitEmail=myemail@email." + type)
                    .Replace("VisitPhone=", "VisitPhone=316-555-" + type)
                    .Replace("customField_tag=", "customField_tag=myCustomFieldTag_" + type)
                    .Replace("customField_text2=", "customField_text2=text2_" + type)
                    .Replace("customField_dropDownMenu=", "customField_dropDownMenu=option2")
                    .Replace("InitialQuestion=", "InitialQuestion=myInitialQuestion_" + type);
        }

    }
}