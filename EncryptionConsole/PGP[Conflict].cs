using System;
using System.Text;

namespace Encryption
{
    internal static class Pgp
    {
        public static void Main(string[] args)
        {
            var encryptionKey =
                Encoding.UTF8.GetBytes(
                "-----BEGIN PGP PUBLIC KEY BLOCK-----\r\nVersion: BCPG v1.50\r\n\r\nmQENBFib/ZsDCACZHCxg2K01vAITRwB530OENQ7D448eLeHRvUiwfjhOBE2ReULL\r\nZiyrUPPwR5uXCO8FaG/QYcuQbhvjOlS2B+i0td9KhDXcJPj6JKRFAp6vg5nZGBut\r\nsL77ni2bvdULz3CfiOVBVQPlq9BPVhyQ9TTCpFO37orUYr4NP5kJ4Eq1ERg9WWh/\r\n444eII5mQIqNAzzwUOcq+xOe0J7jW+IIp8ialAx4ipf+KwPG8KOpb9BxhdTvc9r6\r\nBVkoE9PTtijrenKMspoitypbmwkruPt/4yJxkqlh2LZGJejzS/S+zq9hfQU/LadF\r\nr/hBNhht2LFRiFUb5QZ+Rxftb8yEuWybkJfdABEBAAG0CGJvbGRjaGF0iQE3BBAD\r\nCgAhBQJYm/2bAhsDBAsJCAcGFQoIAgkLBAsJCAcGFQoIAgkLAAoJELLGaWa8T83I\r\n9dEH/1PzYuinnvcXy1blVhegzuKKr+rnkMR81hotxmaBstWNsnRbRuw4JyTFgQ/C\r\n3s0RfS9nUm532s+nDJ/49A1s7zFXBzex6cbDf+eSth/HAJgTPmk8w9svnDdZwTZI\r\n5241746MZ3TZmlQXxj0pIvyQoWJuEFzlZlfKdtx0zKx6pc5oCF1X1sNRTp9jcAJJ\r\nodEtjtDqeYFROwAR2NQ2IGflcHkZxt39WYj+e2Uzo43GVgkgA7J8gALYG/2eBcSH\r\nyk36YPdDsPO0PI7jAzo0ogk+eFx7o5UA5HxvCgjEqDZ5fRhXIV09lZ+1ArTyyPcz\r\ndyR2BX4iHG57lcdpIWKKRMN1NLO5AQ0EWJv9mwEIAI6TtXSueBSDYrbB6zwm+kfc\r\n54rhqS3fWJyJ+9bPFmf3elebfR4w2AqRI7dXOWh5DE6XAtEp/HoBitiLg/7J0hNO\r\nzhFer2hEpdCom6Nkjv56uxmZZdSVfwT/77i9KCsT6bAUiGwa9RDDAMblbyzzT2Aw\r\nSGof92JGgC7/ocvKGtWBY/dqIoHZ5W7xZtU/WXlHm17Q9SVzMBaiaKpVt4bkEpE1\r\nFMMcl4oouIWpjrINFrZSBFSArLgBzv8lB5SP/ykJdPCQxZFfZBSjuuJyEp73tmaj\r\nvkM6WSr5s3kcYj64FdBa44BItME7NnVMHcdEFBTx34sqG2+YwVC/MvVvfmisl8sA\r\nEQEAAYkBHwQYAwoACQUCWJv9mwIbDAAKCRCyxmlmvE/NyDkBCACRTGVTRleW8XAt\r\nqYTDUYAdcdYQET8v3bG9DdBP9dTv6DcXpM5KTxwp/efps3pKwQ+tJaG9KUGSJE4x\r\nL7DqRb0ZOdefbE2J/lK7Q4p7pUlzKOhQcoHqDdp9ag7k6hT4PBYmAkWG04Pg//Hy\r\n5WMRL9BYbU9I0rumNmFSkmiXyTFJUIL1UySIp+L9YdAjeTl1ZJ7o/CePmZharxuy\r\nB+VwevI57qNaQx58DsqbSuWSW6TThcYOio0S1Oq8n/3Wg+eXyIRxB5Wf9NRzB25I\r\niMCjDis1J+rLwNefI4IfVLDzkFQMTCN/zkah++t/ahe97SF966ehHbfVtDolIlMB\r\nMKFl757p\r\n=dKdT\r\n-----END PGP PUBLIC KEY BLOCK-----");

            var signingKey =
                Encoding.UTF8.GetBytes(
                    "-----BEGIN PGP PRIVATE KEY BLOCK-----\r\nVersion: BCPG C# v1.6.1.0\r\n\r\nlQOsBFicldoBCAC1GUGvKKjYBBNNbSlB6EX7rJbvQJ86yKuN+RX0MITlPNCfgpTY\r\nDuMAFimJISiJvj+Df0ja52ZEgBsKciZ6h5pqMkkKDTfc1/i6fHBwpsWFtraybcAT\r\n/Pnl2Tc4FnpHYAGQ5sv1s5ICTSDuKxc1OS2xFEkBUkbnUt+0wOHlxlNU1XcRRwnX\r\nS+SxvDJkM+I+7nbQL+iBeSFxJw3Ts4h9x5GHzyvTLuPiXt/HCXgaTYwwkFg3mrer\r\nEr3qWowyAqH2b/6sQ2oYQx8Fia4dLxNf4NWtJp/jC1zLrlgbGZrA3QJ9EhzrpYdg\r\nbhad525q1x6XzsVztgTV8renGC654lN+OmbdABEBAAH/AwMCkUcuViaBnPVg60Kj\r\nOHSe0BWIyYY9jkmctgZw8JuuzpRbYhtRButnsZ/n13y9ZYadcB42cTaAn70ATocT\r\n285W71HnfdaDFy/vP0c3+TrktQeVjrIbH8L68bkEkvzalI3gJGaefIXN87cBcy1A\r\n8vPrQmUx0HsveuMnc9gOLMrMc0cvF+/K8m+LSt8NskYSMnKWBNwXcL0aDx6oG0jZ\r\nLlFLjCSJfkV/L5KcYhE63so/bVkDGMNVQDWCNS2CEDFouJUluVL958Z7KzJGr7sp\r\nZaBluWdVFxwAgiC1StyJ+8SRx6fT4lNfXiLr0yLzOc+JdbRh/N4bU0qpyDB8l6ie\r\nA4EisjHQ8zTKcQJxd8nYlXzwYugFSP6UC6KZ4yFDkct4S4SU/ErIymNuLEkj0hFy\r\nZpVdhNx+JGSoGAAR05FOZw4P7NnMO9upm+bgwwzUa9UcXctwpuNE77jQa3QuB5gK\r\nMEdAKPbAKorMXLvKaxodJDg0Fga8o5keA53KE1dZdqcNcudhWH+wyr6qJ2hpJDYl\r\nIBWAZVn1xXCnHQAnMJnkWyw1yoXhUW/66+u/SbKzSDCpSfzqxf1waCKJ80Lywx66\r\nr2Vaql+YiJBiD92D3Uuc4Qk30j0ie5oYcfs3eUUOZOosV3tDB/PMa7XTtVKBA5fC\r\ncngydAho+X5YQRFZI4rjOUGQk15+rExx9GGAF0vErl73B4z/bQ5Flrt1Xk7G4HhU\r\nFA4RNhuHDX/E1/IWccjfygb3YmztrimHaAXzNmXcNvR4F9z47mrEvyc9n+cJNtQJ\r\nMrQqZNi4uIOd21I1YAdmQ5ZYqwKOwS7saAvh0JgO998G6x4LSRDagRuc48Re3WN9\r\nxj79emIS9isD+rd1F7/MTMdqCLpsKrFtGuuUOTpFs7QAiQEcBBABAgAGBQJYnJXa\r\nAAoJEGScNRVl3kuGepYH/0snprd2KiTdz4mdDombhLLROgw/Ji3vsp3CbFuFOMhb\r\n5mp3OQEkBsqJ7x66hIMx4Ry/MKHUji2IWVvTlGhhmxblZVlErRpLzw20YLIuRQDC\r\n+bJJJ9y8HorCfjkya9VxOWB1xAXwV3xxKY5eVvSWLK/8YhGbLPwfcJVxsXCaHVnw\r\nKZaalffV11nBwRYWa9VKGxrlIjS98ym8ANfpz8JlhA/B68oCqSKtv5moJkRUO7qD\r\nFRnEjBW0SQY87+cjB5BJJ2ekH4Fzpqce9VrnIcLtmF4xq8JxjLfGFymkeYhhsTY2\r\n4Aik5kUXyAMZtYOCpLs7tIi7TeYJktwBlCxD1FTkOgw=\r\n=NKpY\r\n-----END PGP PRIVATE KEY BLOCK-----");

            var pgpUtil = new BoldchatEncryption.PgpUtil(encryptionKey, signingKey, "12345".ToCharArray());

            const string commonParms =
                "VisitorKey=&Expiration=&URL=&ReferrerURL=&VisitRef=&VisitName=&VisitInfo=&VisitEmail=&VisitPhone=&InitialQuestion=&CustomURL=&customField_tag=&customField_text2=&customField_dropDownMenu=";

            var visitParms = "Type=visit&" + getParameters(commonParms, "visit");
            var floatingButtonParms = "Type=chat&ChatKey=&FloatingChatButtonID=2540228524023957271&" + getParameters(commonParms, "float");
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