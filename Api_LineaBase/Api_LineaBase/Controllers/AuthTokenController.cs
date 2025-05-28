using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.IService;

namespace Api_Empopasto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthTokenController : ControllerBase
    {
        private readonly IAuthToken authToken;

        public AuthTokenController(IAuthToken _authToken)
        {
            this.authToken = _authToken;
        }

        [HttpPost]
        public async Task<IActionResult> GeneraToken(AuthModel authModel)
        {
            string vToken = authToken.GenerarToken(authModel);

            return Ok(new AuthResponseDto
            {
                IsAuthSuccessful = true,
                Token = vToken,
                ExpiresIn = DateTime.UtcNow.AddMinutes(10).ToString(),
                TokenType = "bearer"
            });
        }
    }
}
