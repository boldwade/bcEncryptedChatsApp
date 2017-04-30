using System;
using System.Text;

namespace Encryption {
    internal static class Pgp {
        public static void Main(string[] args) {
            var publicEncryptionKey =
                Encoding.UTF8.GetBytes(
                "-----BEGIN PGP PUBLIC KEY BLOCK-----\r\nVersion: BCPG v1.50\r\n\r\nmQENBFi93mwDCACXYTskZy5oOboVEKSaDLDIHYOaPFH4xqMtXmv134P3dQn9pCtF\r\nng6kfO76Fes5FzZ0j4aIfKF6wlzkRJ63p/5z6NL1Gp8hHV5pR24LcF/0FWdMcweN\r\nWw/YcH2u1kg79h21hq4sW2BRQskOWNBkC3EAT9mbkpnmfQVqVRZfFzw7hvVsmt5R\r\npnxxP3rd6m97TmCiTU9WRufGciEmUcK7PLpuvAojmdnfTxBHcl8fyeDZ+OeGwu5E\r\nCOv47kBV4r/MpKdUYycS2BnB149PGiQyU2cdsItfayERFUDuTPj2lg0uq92SMSO8\r\nKmo98SjEzMOWzVyTtJW5cF3IajstLxinsGTlABEBAAG0CGJvbGRjaGF0iQE3BBAD\r\nCgAhBQJYvd5sAhsDBAsJCAcGFQoIAgkLBAsJCAcGFQoIAgkLAAoJEKBpH1w+pRHZ\r\nWYUIAIM0Sb5BuW2fJMAGa6VnOwD+oLIgxbNrt1LJpf3cvDJPnEdt9cIVqifaJb0f\r\nl7Yun1JXLwA7HMPfD8S76Ejha38NUUc6BSdFClX1Gz5ARDObDVDK+m61RwLpYdXH\r\nwm0y23PkLC0XKN8K3c+d/HnZnZV5pezh5VI/vrZMhXhwenedtB03ahHooVSifBjE\r\nfKq7SRjuGxu5UOVnk/pth7l1+2eWwJQqhL1glv+2kQ728jc3eNCci+ODwQneU4M0\r\nTgJbvg3V2D6cq6tuvHJOTbJ+ckaN3t9wXCLCCag5TxWwmUvjC398H9UieIP+a8E+\r\nAOWm+gCJ75rXMpVntHzCIfE6MiK5AQ0EWL3ebAEIAMtOpCduTOw9TUgNto8EDcjt\r\nrM6PRp81A58pTUPlbUCsK5/YTgkCd2tGhnqbgzWraWDhcBX6uy78ete5bBrfi+N+\r\nctbP3pQbIgZUTQH8lrKSTRFL5MvVrTlVGTAt0CFrIRGiczcWWdh59d61QIZEbiXr\r\nSLK29AJcbSyWzC8f0Z+ZUq4a33c4lRfj+s7mLiBjWUxUQtaaL9mJreMSfw6eYXNV\r\nfrUItaR/pOvqnPTzb/wB+O0B2VnSYICWdvsRM4Vh+3d/JJxBKH8724+f5wvs3joZ\r\njtNxh0zTOMrw1k7k0+OFcAE+YI2WcbzsyeR4yZwhXxAcJlqvgbljXjtbngFaOfMA\r\nEQEAAYkBHwQYAwoACQUCWL3ebAIbDAAKCRCgaR9cPqUR2dbyB/4wFKiGL9WE0SZ9\r\n/0DKklaPqSECput2CJxa8pYYW0lF9q3f/r+mpGGqyk23SBsWt/qfBhbEW8501TiI\r\nSIkfjitOV/OqlUdGCidzBsTV5wOeCZW8fBlJ6jXsFGwYoZIwQTesFnMF8kLf+3ec\r\n5u8mAithzjaJiZksaL+j//M++cLSKhS6kOgA93lMLpJ4R3VeT+WqEusEi2M4WaFJ\r\n9icHaBe/3+khYnzx6QTeiNPDam4PSfOak4dotyQTChhR6Z8Mz9ATbX84I1WeiHNz\r\nYvJdp2gCrRR2prDJ2+sVUYmNCNBKiIVZkHmhq3SOyZLOoAatRWCO2uH2PrP1249d\r\ntNxKuQwo\r\n=GgWM\r\n-----END PGP PUBLIC KEY BLOCK-----");

            var privateSigningKey =
                Encoding.UTF8.GetBytes(
                    "-----BEGIN PGP PRIVATE KEY BLOCK-----\r\nVersion: Keybase OpenPGP v1.0.0\r\n\r\n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\r\nf+5wT7zL\r\n=tSdx\r\n-----END PGP PRIVATE KEY BLOCK-----");

            var pgpUtil = new BoldchatEncryption.PgpUtil(publicEncryptionKey, privateSigningKey, "12345".ToCharArray());

            var unixTimestamp = ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + (60 * 60 * 24)) + "000";
            var commonParms = "VisitorKey=&Expiration=&URL=&ReferrerURL=&VisitRef=&VisitName=&VisitInfo=&VisitEmail=&VisitPhone=&InitialQuestion=&CustomURL=&customField_tag=&customField_text2=&customField_dropDownMenu=";

            var visitParms = "Type=visit&" + GetParameters(commonParms, "visit", unixTimestamp);
            var floatingButtonParms = "Type=chat&ChatKey=&FloatingChatButtonID=2540228524023957271&" + GetParameters(commonParms, "float", unixTimestamp);
            var staticButtonParms = "Type=chat&ChatKey=&ChatButtonID=8200206508610860737&" + GetParameters(commonParms, "static", unixTimestamp);

            Console.WriteLine("Encrypted Visit SecureParameters: ");
            Console.WriteLine(pgpUtil.GetEncryptedString(visitParms));
            Console.WriteLine();
            Console.WriteLine(replaceNewLines(pgpUtil.GetEncryptedString(visitParms)));

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Encrypted FloatingButton SecureParameters: ");
            Console.WriteLine(pgpUtil.GetEncryptedString(floatingButtonParms));
            Console.WriteLine();
            Console.WriteLine(replaceNewLines(pgpUtil.GetEncryptedString(floatingButtonParms)));

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Encrypted StaticButton SecureParameters: ");
            Console.WriteLine(pgpUtil.GetEncryptedString(staticButtonParms));
            Console.WriteLine();
            Console.WriteLine(replaceNewLines(pgpUtil.GetEncryptedString(staticButtonParms)));

            Console.ReadLine();
        }

        static string replaceNewLines(string text)
        {
            return text.Replace("\r\n", "\\r\\n");
        }

        static string GetParameters(string parms, string type, string timeStamp = null) {
            if (timeStamp != null)
            {
                parms = parms.Replace("Expiration=", "Expiration=" + timeStamp);
            }

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