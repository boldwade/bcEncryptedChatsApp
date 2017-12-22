using System;

namespace Encryption {
    internal static class Sha512Util {

        public static void Main(string[] args) {
            //const string hashKey_alphant2 = "yz55YHEQb8bBUDZXfuCZVYylvBSejyIS9CdoD8ISQKUZA/YxNg5n8FEsAwT/aE7u";
            const string hashKey_dev = "8MfjTXFWYEUxSXQgcpV7u/7TV9FdRpR9siuodSTTzvNiQPW3mEbjj69N/A/rHf5s";
            //const string hashKey_aid231 = "VZxqk02K6C73M9Qa5uvjO85fkj4W8O3EvPV+cOUWMOpGkdXBvhRpnzPTAEZgG9ZV";
            var shaUtil = new BoldchatEncryption.Sha512Util(hashKey_dev);

            var floatingChatButtonId = "";
            var staticChatButtonId = "1790964020649289";

            const string commonParms = "VisitorKey=&Expiration=&URL=&ReferrerURL=&VisitRef=&VisitName=&VisitInfo=&VisitEmail=&VisitPhone=&InitialQuestion=&CustomURL=";

            var visitParms = "Type=visit&" + getParameters(commonParms, "visit");
            var floatingButtonParms = "Type=chat&ChatKey=&FloatingChatButtonID=" + floatingChatButtonId + "&" + getParameters(commonParms, "float");
            var staticButtonParms = "Type=chat&ChatKey=&ChatButtonID=" + staticChatButtonId + "&" + getParameters(commonParms, "static");

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
                    //.Replace("customField_tag=", "customField_tag=myCustomFieldTag_" + type)
                    //.Replace("customField_text2=", "customField_text2=text2_" + type)
                    //.Replace("customField_dropDownMenu=", "customField_dropDownMenu=option2")
                    .Replace("InitialQuestion=", "InitialQuestion=myInitialQuestion_" + type);
        }

    }
}