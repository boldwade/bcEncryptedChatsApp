using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace WebAPIForBoldchat.Controllers {
    public class ShaController : ApiController {

        // POST: api/Sha
        public HttpResponseMessage Post([FromBody]JToken data) {
            if (data != null && data.HasValues) {
                var hashKey = ((JValue)data["hashKey"])?.Value;
                var secureParameters = ((JValue)data["secureParameters"])?.Value;

                if (hashKey != null && secureParameters != null) {
                    return Request.CreateResponse(HttpStatusCode.OK, new BoldchatEncryption.Sha512Util(hashKey.ToString()).GetEncryptedString(secureParameters.ToString()));
                }
            }   

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Missing Paramaters");
        }
    }
}
