using System;
using System.Text;

namespace Encryption {
    internal static class Pgp {
        public static void Main(string[] args) {
            var publicEncryptionKey =
                Encoding.UTF8.GetBytes(
                "-----BEGIN PGP PUBLIC KEY BLOCK-----\r\nVersion: BCPG v1.50\r\n\r\nmQENBFi1z7MDCAC25OR80O9raa6wkA/L2uiyabwGVk9KWtXKjKUBeNJnVpiqtn+W\r\ng4r40RvhKtndiK7s5VnNYvwemykbNwsjxZd80Nx4SzPjfoX5Xu/oGGd86rb5poDD\r\nBx2fCEfFzmqAn2MqpFJLFEOi8HsuMRW6PFD0fOPmf4o+rz+wXYH8qQ8opjcKIGrB\r\nLYbG8pksFbloLaUzJKNHCqOnX36b08o160g9Chm76NvAxvZwQc1tJWLKq9oAg7E7\r\ni2WVdQzTamVf5GI2ZqEmmbWkBfW0qRWovhHKh/EeOkh4MiN0eUlNViG4+UHv3ORS\r\nfyHVn40fkIFsiX3gVBijIiHA8voUfRbNfnM5ABEBAAG0CGJvbGRjaGF0iQE3BBAD\r\nCgAhBQJYtc+0AhsDBAsJCAcGFQoIAgkLBAsJCAcGFQoIAgkLAAoJEFKit0JzjL8j\r\nE6MIALZ9LPHlXjrtgXzHCZqyjtBNXwLIXrpdZuJAxESeS9wft5QNTpS0ZsBVOad1\r\nK9qh0Oos5GgYnvwU03dC4TQX94zh10AoLrnctoHocJTwqk5Ph9uGzw13gUbSazXw\r\n2qQDO2GJhfs0B4IwC2L2Ibrw9b4YunEH4z4lNoG2/Twe9nAErGjUULdsVIlpOfNr\r\nZ/OAj7J08ZLpLvAlpsi4Cfyy2lndXDyY0Q+1jg/rRfVp4a9QtugXttdZS8rSvImh\r\nyuXvuURO42shiX/rqqlwYYhKa8quO2GzUzQdpwgU+QABUSOJTUQiiFPJIRdqxzlu\r\nSO749jbUMGaXcFLtLPrufhoAKUq5AQ0EWLXPtAEIAN0YssXecoYS5/+FO76GNGln\r\nIYc/dQqHqKex9sScrZRW4riXhR320IMXjbRdWLYUnELPcnIxGSG/IYhxy1B0rbfx\r\nlJj+CkIhkwZfkROz+xB0ZNyclDQk0F5CLksxS74QzhTpIihuFkkvHkmE3AJyQsYJ\r\npSFA0xluxG9NReBaz9UFzcIlhtGkyeAli8OAxssxn5+tNVl39hCmHi16YqSZB5/8\r\n7p37HqoXkD3yg/q7oGXRPS4jAotnm2WgBIGqJH6Nv8QnYheZnTzFjpjwo804YOK4\r\nr/lJIuPhF/cCKUQweyNufUQp3GiYXMLs/g+ibpmYVj+dG9AiB/Vwv5AEjAKdMdcA\r\nEQEAAYkBHwQYAwoACQUCWLXPtAIbDAAKCRBSordCc4y/IytWCACh5Paubv7NlvOD\r\nr9qgXjn4DFxL6PAE0n2T5SjUkLdlvmF6KZt/7MeVtgA7VWxIqiLiGCBTw2+0ywV+\r\n0aqkwy0sV1PfSP3rq+UlU4RAYOywOvGzzxfhS20MlTVASOLAPvfxSzeKAaKXjWcg\r\nu2HCG1tst6JwRJTW1+JYDx1Uz0xux5mSc1iddPKPKy30pZsoBTuiGYwom4+assSc\r\np5Rs/VWjbKE9VC/lqg0Z4Y6VHl5fV9QC18yuWAdqo8l/mMzATXlKOBsf216WIH3F\r\naTF7sPDy0ewojep6kBjPevYRcrlBAuBGKHhNnj/N2OAnk6we+1PH+fZdgc18M+EP\r\nI94Bcvtb\r\n=u8iy\r\n-----END PGP PUBLIC KEY BLOCK-----\r\n");

            var privateSigningKey =
                Encoding.UTF8.GetBytes(
                    "-----BEGIN PGP PRIVATE KEY BLOCK-----\r\nVersion: OpenPGP.js v2.0.0\r\nComment: http://openpgpjs.org\r\n\r\n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\r\n=3Ip/\r\n-----END PGP PRIVATE KEY BLOCK-----");

            var pgpUtil = new BoldchatEncryption.PgpUtil(publicEncryptionKey, privateSigningKey, "12345".ToCharArray());

            var unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + (60 * 60 * 24);
            var commonParms =
                "VisitorKey=&Expiration=&URL=&ReferrerURL=&VisitRef=&VisitName=&VisitInfo=&VisitEmail=&VisitPhone=&InitialQuestion=&CustomURL=&customField_tag=&customField_text2=&customField_dropDownMenu=";

            var visitParms = "Type=visit&" + getParameters(commonParms, "visit");
            var floatingButtonParms = "Type=chat&ChatKey=&FloatingChatButtonID=4568580131668857174&" + getParameters(commonParms, "float");
            var staticButtonParms = "Type=chat&ChatKey=&ChatButtonID=8200206508610860737&" + getParameters(commonParms, "static");

            Console.WriteLine("Encrypted Visit SecureParameters: ");
            Console.WriteLine(pgpUtil.GetEncryptedString(visitParms));

            Console.WriteLine("Encrypted FloatingButton SecureParameters: ");
            Console.WriteLine(pgpUtil.GetEncryptedString(floatingButtonParms));

            Console.WriteLine("Encrypted StaticButton SecureParameters: ");
            Console.WriteLine(pgpUtil.GetEncryptedString(staticButtonParms));

            Console.ReadLine();
        }

        static string getParameters(string parms, string type) {
            return
                parms.Replace("VisitRef=", "VisitRef=myVisitRef_" + type)
                    .Replace("VisitName=", "VisitName=BC Test Name_" + type)
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