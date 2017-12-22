using EncryptionApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EncryptionApi.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShaController : Controller {

        // POST: api/sha
        [HttpPost]
        public IActionResult Post([FromBody] Sha data) {
            if (data?.HashKey == null || data.SecureParameters == null) {
                return BadRequest("Missing Paramaters");
            }

            return Ok(new BoldchatEncryptionUtil.Sha512Util(data.HashKey).GetEncryptedString(data.SecureParameters));
        }

    }
}
