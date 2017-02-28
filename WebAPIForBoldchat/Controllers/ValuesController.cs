using System.Collections.Generic;
using System.Web.Http;

namespace WebAPIForBoldchat.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            const string hashKey = "+N2HlELtLtpx6N2kWNNLb8fsIdj0dige9xEGB34RvybUzxRkQhkTmI30kSYVN4Mc";

            const string inviteParms = "Type=visit&VisitorKey=&Expiration=&InvitationID=890769783384352&URL=&ReferrerURL=&VisitRef=&VisitName=&VisitInfo=&VisitEmail=&VisitPhone=";
            //const string chatBtnParms = "Type=chat&ChatKey=&VisitorKey=&Expiration=&FloatingChatButtonID=890770284654439&URL=&VisitRef=&VisitInfo=&VisitEmail=&VisitPhone=&InitialQuestion=&CustomURL=&LanguageCode=";

            var shaUtil = new BoldchatEncryption.Sha512Util(hashKey);
            return shaUtil.GetEncryptedString(inviteParms);
        }
        public string GetSha512(string input)
        {
            const string hashKey = "+N2HlELtLtpx6N2kWNNLb8fsIdj0dige9xEGB34RvybUzxRkQhkTmI30kSYVN4Mc";

            //const string inviteParms = "Type=visit&VisitorKey=&Expiration=&InvitationID=890769783384352&URL=&ReferrerURL=&VisitRef=&VisitName=&VisitInfo=&VisitEmail=&VisitPhone=";
            //const string chatBtnParms = "Type=chat&ChatKey=&VisitorKey=&Expiration=&FloatingChatButtonID=890770284654439&URL=&VisitRef=&VisitInfo=&VisitEmail=&VisitPhone=&InitialQuestion=&CustomURL=&LanguageCode=";

            var shaUtil = new BoldchatEncryption.Sha512Util(hashKey);
            return shaUtil.GetEncryptedString(input);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
