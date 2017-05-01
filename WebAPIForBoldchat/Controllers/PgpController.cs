using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace WebAPIForBoldchat.Controllers {
    public class PgpController : ApiController {

        // POST: api/Pgp
        public HttpResponseMessage Post([FromBody]JToken data) {
            if (data != null && data.HasValues) {
                var encryptionKey = ((JValue)data["encryptionKey"])?.Value;
                var signingKey = ((JValue)data["signingKey"])?.Value;
                var password = ((JValue)data["password"])?.Value;
                var secureParameters = ((JValue)data["secureParameters"])?.Value;

                if (encryptionKey != null && signingKey != null && secureParameters != null)
                {
                    var pgpUtil = new BoldchatEncryption.PgpUtil(
                        Encoding.UTF8.GetBytes(encryptionKey.ToString()),
                        Encoding.UTF8.GetBytes(signingKey.ToString()),
                        password?.ToString().ToCharArray()
                    );

                    var result = pgpUtil.GetEncryptedString(secureParameters.ToString());
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Missing Paramaters");
        }
    }
}
