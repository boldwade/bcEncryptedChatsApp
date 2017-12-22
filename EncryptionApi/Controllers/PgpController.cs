using System.Text;
using EncryptionApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionApi.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PgpController : Controller {

        // POST: api/Pgp
        [HttpPost]
        public IActionResult Post([FromBody] Pgp data) {
            if (data?.SigningKey == null || data.EncryptionKey == null) {
                return BadRequest("Missing Paramaters");
            }

            if (data.Password.Length == 0) {
                data.Password = "";
            }

            return Ok(new BoldchatEncryptionUtil.PgpUtil(Encoding.UTF8.GetBytes(data.EncryptionKey), Encoding.UTF8.GetBytes(data.SigningKey), data.Password.ToCharArray()).GetEncryptedString(data.SecureParameters));
        }

    }
}
