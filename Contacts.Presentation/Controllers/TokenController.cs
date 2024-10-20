using Contacts.Domain.Entities;
using Contacts.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly iTokenService _tokenService;

        public TokenController(iTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult GetToken([FromBody] Usuario usuario)
        {
            var token = _tokenService.GetToken(usuario);

            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized();

            return Ok(token);
        }

    }
}
