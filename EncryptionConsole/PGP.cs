using System;
using System.Text;

namespace Encryption {
    internal static class Pgp {
        public static void Main(string[] args) {
            var publicEncryptionKey =
                Encoding.UTF8.GetBytes(
                "-----BEGIN PGP PUBLIC KEY BLOCK-----\r\nVersion: BCPG v1.50\r\n\r\nmQENBFkHf2ADCADGWFmH7CydRP/BxN2s8PVrAuz5IgDqvjsxsXHq4FcVgN3xnWzA\r\nC//Z+6MYd+FlpSdKFc+KoPk6+ZlllWPI6CCkSQhbPpr2OqHqNZz10mVuMBhRpVUO\r\n9+tk3VyGi1gmkOa0WkFZGxPgfG2rosOettef2UIOTqV54XxANHKRuLRgEiUzN0GI\r\n/vntgQVHr4XxY7G2dAMv5Hr7I1y5vJ+7RGrNM5+XA7nACTIK5QLXhD+JAjxp82AL\r\ndBz+ubZob9ik4Sblh53hSYOh4l04Ix7TlbeZy4aw3GfLUumiDXt2UgJeCcB0SzPn\r\nuYhpjgz/P52pUdVi6r2+PQ/MDZ+Y041ldO8DABEBAAG0CGJvbGRjaGF0iQE3BBAD\r\nCgAhBQJZB39gAhsDBAsJCAcGFQoIAgkLBAsJCAcGFQoIAgkLAAoJELJX9yj3Ldix\r\nz7IIAKzqG8N8GyjIey4LGkzyWZ+irdFHDlwd/gNptxo1z9UpNXvjLnhSDxaHDIM6\r\n/hcbhmNPOMTixGmg36HwC5ldj/koDJKnPiCZEndcvSdiqDFGLmivoWK0/hnqno4n\r\n+J0r/OQkXJtwf7iWwTB1womyThgkjCzo7JckedgyULknZPcTebrw451KlTjuY6DB\r\n94AUWoGy6xoMLFn6e9JFRsvTK1ULJ2XfSy57YJlttxAr4H7FCB5eXQnemFJB2juo\r\nsKsEISU70956GDPH4DxJeBGPq+Z1VU07SYifL711nkH0mHEQGRoxo6tSFgJwgXR2\r\nnLW+Ve/Pt+JPe0cUnzXC/q6vDRW5AQ0EWQd/YAEIAKinjFeDpSig+gRoyRDSceGC\r\nU8FQ63IEg6Wg2cfBXRfw2Vh4jN8BMtqBaeW8QJiX8JjFCXeND1/E/FeMyU2LVA8e\r\nCtIaH2S98s8bPjyN/UqGHbnw9MC6maOnNgX60JFOZgohnjxrvDkkxfOUaLLFC6G+\r\nPw+T9blSQsvf0wRTRFZ3eF5cPP2sMW97eJK1+aF/Ga3WtKchRNZ1iV0aY1pRKUYH\r\nJm74ugCa0qT0QaKYNUVhZJ9QCmZGsob4AVMa8HtLfuaEvTR8kNUaXEVdHHOQP2B0\r\n+TCOWxZn0qBZlZ3EH0dig3T9+msAKUFFk9iR1vNX/fYkWJNYmzOLJzXvbl3L+i0A\r\nEQEAAYkBHwQYAwoACQUCWQd/YAIbDAAKCRCyV/co9y3YsaRHCAC9L++AGIEsXJrO\r\nkWtBANHv5xABjb6ec/QNaqMXUfJABsbFDmIhUhoV9BMafkM55e3yPk4XfTL6xQ90\r\nAhZRxOSr39GgK4Pms5DdAXJ70ZONB8Paw6ttGT1QttMfNGuS4fKtx7QnGkHeBkdi\r\nrWb/3+cFWI5pcuNmj0m6yOwFpex67gBI4q/F5YTXQJq4fGMHBaTxMGOdPXHvc+P1\r\nMFsBUjcw/3rMsVUMd+ybP42enBPHYM2oQ8jaHapG0j0949uiY9ZPWqAduRtK8xeq\r\n4hfOtL378QuG+L39HumSSbk/hufJSnLV91G8GmlTF6NCeqY3VHdFMnGwmM4ZsW6B\r\nnlSHoMHW\r\n=2An0\r\n-----END PGP PUBLIC KEY BLOCK-----");

            var privateSigningKey =
                Encoding.UTF8.GetBytes(
                    "-----BEGIN PGP PRIVATE KEY BLOCK-----\r\nVersion: BCPG C# v1.6.1.0\r\n\r\nlQOsBFkHf/sBCACO45TOv4Ka601DmblYy9ih1Fi6jpNg2zSvPPp76lZN/m6D5iGj\r\noGCkw4qDp/IzSTgJXgvAnTVIRXyZCTwhw8UBfAXvXWbwap2HBlBoe0QyihT1W58W\r\n2M9jxFD2LRr13h/C1IprL2TVxyf41/39HmqgPXCcngI+tRUM+8DY93SUsAXt5yIQ\r\nCRZFdxRye6KHsCOT3prlP2UdUyNX05ZW3xDC8Krx+0y4AVcoEi1sR01WzFiy/VNz\r\nyvTVRFSJiLoZP6su0BFKMAT2suXyP3TjluyZ8Erf6AIuNnbsLotR//Kytz+4P5nk\r\nDJuk5g1zPHbA2p3lscu+dNfkGBPemjDQgWIhABEBAAH/AwMC7PPwmxzjiv5goTr7\r\nVBUlvqBVy1NlNLYT249ItygqU+UciQv1gedOlqbb8XKk/RLzjLJjMAX1vf2PRHAK\r\nIKIugMIM6fJLbqOu0cRhU0w3gM9OfTlfa2m/XYsfZ0JdC5EZbjFhz55WOSA2L3iI\r\n6vbu3tpoE6VOtA5IuJyMgocnt2qoB0JgaOLoLI7zlvJNDqrW0kc6G/rQoabbPyuq\r\n4+p9UM7FkK6zi/eIt0l6/t/M2wwC7alM/3j84vjn2k791tf9fmd2C/N0xYJCyejq\r\nsQ1yx4u+I5m0U9PrAJApS9wWYGg3UmM7wkGnCuNg1eojpTndlHl0Vht433GRRznu\r\n2I+qdJh9Oj0Z5RZ0xqHhubSslVpzzOFM2sQElzaOxsBSZ0N3kBEDsYMDs5tdabDP\r\nU+IB/r+I5PxszBwzfrhXEixNoiAlFY/oSXIm2nAwvOW2h9eOcxjb6CxJQuTljotY\r\nD5U4csyM9Ui6pS4vQOWhzRIpHezx4rjjNbzr25nqcSEqaydYO6utWuqsJ3ZTv68U\r\ngkUFIt2XPxXxQCUZzGzqyOLRK6oDWp1gX6NM7d/xVCEBbdEM4aCAroAjWDEQrH0J\r\nud3BCX/mD44/rzqt1lhQxpKB7Xr1YCtihOw0WRlA1jJoMiK1Ko7XDOM7Rp1DqWLx\r\ntpmvGpRUkAKQ582dGHTMnAiwpJl4e1Gu/0ya/GJGCaVMQOX8/CHD3Zj4eAgrwBNJ\r\nqIoulxrZo/vEett7ukFymuSag1XcTWUQLcVujCXT0jUwS9JkhglHBknySRRQXZxi\r\nJU9/2gBMC1BBfSZwqQTezEX6XbITzjEO/LLVoyM+h+9qLs3yhirNGDocMHzghres\r\nZL3LzgFJgsQxeOkxuvx/quvXEl9lNJ+BlHcdJyn4qbQAiQEcBBABAgAGBQJZB3/7\r\nAAoJEKIJ8AjET039az0H/2gU8Qig+z9BUhDGdt0L54vE6my52Ukq8u61XKkORXAV\r\np8Fjzv35j4eiaw0VhS5lnBp+WWd0ln7LmqBGNo6XT0ugNhyFjW7epangnLmhklsi\r\nuYgh2EJXjXMaiKHv9/vQMqqnbK9l9mswKOUXgLhkJfTfrzt+37eL9/Jwc5eA0f7t\r\neXeAyrK2aT6jmS1rv6rXaSm02/xnh5FfFZZ8gacg53eVSBfSidAiViUg+w1mpTCQ\r\n8Oiy4e7dAQe9RnJ29+Sh5a/busNtlX5qmPbE+a4NmPZdPIx0bUafE2zvBOvy+Vby\r\ntTmSmx7SrUeAfSWcPqyD4yoOXGxqsAfVWCwJ4bm493E=\r\n=hKzD\r\n-----END PGP PRIVATE KEY BLOCK-----");

            var pgpUtil = new BoldchatEncryption.PgpUtil(publicEncryptionKey, privateSigningKey, "12345".ToCharArray());

            var unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + (60 * 60 * 24) + "000";
            var commonParms = "VisitorKey=&Expiration=&URL=&ReferrerURL=&VisitRef=&VisitName=&VisitInfo=&VisitEmail=&VisitPhone=&InitialQuestion=&CustomURL=&customField_tag=&customField_text2=&customField_dropDownMenu=";

            var visitParms = "Type=visit&" + GetParameters(commonParms, "visit", unixTimestamp);
            var floatingButtonParms = "Type=chat&ChatKey=&FloatingChatButtonID=2540228524023957271&" + GetParameters(commonParms, "float", unixTimestamp);
            var staticButtonParms = "Type=chat&ChatKey=&ChatButtonID=8200206508610860737&" + GetParameters(commonParms, "static", unixTimestamp);

            Console.WriteLine("Encrypted Visit SecureParameters: ");
            Console.WriteLine(visitParms);
            Console.WriteLine();
            Console.WriteLine(pgpUtil.GetEncryptedString(visitParms));
            Console.WriteLine();
            Console.WriteLine(replaceNewLines(pgpUtil.GetEncryptedString(visitParms)));

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Encrypted FloatingButton SecureParameters: ");
            Console.WriteLine(floatingButtonParms);
            Console.WriteLine();
            Console.WriteLine(pgpUtil.GetEncryptedString(floatingButtonParms));
            Console.WriteLine();
            Console.WriteLine(replaceNewLines(pgpUtil.GetEncryptedString(floatingButtonParms)));

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Encrypted StaticButton SecureParameters: ");
            Console.WriteLine(staticButtonParms);
            Console.WriteLine();
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