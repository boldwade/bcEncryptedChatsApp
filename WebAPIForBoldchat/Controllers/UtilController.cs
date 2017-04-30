using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BoldchatEncryption;

namespace WebAPIForBoldchat.Controllers
{
    [EnableCors("*", "*", "GET")]
    public class UtilController : ApiController
    {
        //private readonly Sha512Util _shaUtil;
        //private readonly PgpUtil _pgpUtil;

        public UtilController()
        {
            //const string hashKey = "+N2HlELtLtpx6N2kWNNLb8fsIdj0dige9xEGB34RvybUzxRkQhkTmI30kSYVN4Mc";
            //const string hashKey = "KZP92SRWYnqO7vUAlQZ7WXOT8+JQyL5P4t4zolqttK2gBfEty/tJvIaFuoOqRF4h";
            //const string hashKey = "rL7IT0hFxPznG1LVno6ljBzGZnwnWj81TcQpxqEqf74iYUKwo39IRsi6/n0NwlZ7";
            //const string hashKey = "ql8mUeeCDFfz/qzCQd/RGNML2FmeMBWNoE9QJ+dfbJKm1BFE3AZ6pGkqk833Bubx";
            //_shaUtil = new Sha512Util(hashKey);
            //_pgpUtil = new PgpUtil();
        }

        // GET: Util
        public IEnumerable<string> Get()
        {
            return new[] { "value3", "value4" };
        }

        [Route("api/sha/{hashKey}/{input}")]
        public HttpResponseMessage GetSha512EncryptedUsingHash(string hashKey, string input)
        {
            if (string.IsNullOrEmpty(hashKey))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "You must include a valid hash");
            }
            if (string.IsNullOrEmpty(input))
            {
                input = "ThisIsWorking=sha512";
            }

            var shaUtil = new Sha512Util(hashKey);
            return Request.CreateResponse(shaUtil.GetEncryptedString(input));

        }

        //[Route("api/pgp/{hashKey}/{input}")]
        //public HttpResponseMessage GetPgpEncrypted(string input)
        //{
        //    if (string.IsNullOrEmpty(input))
        //    {
        //        input = "ThisIsWorking=pgp";
        //    }

        //    var pgpUtil = new PgpUtil();
        //    return Request.CreateResponse(GetPgpResponse(input));
        //}

        //public string GetPgpResponse(string hashKey, string input)
        //{
        //    return new PgpUtil()
        //    return _pgpUtil.GetEncryptedString(input);
        //}

    }
}
