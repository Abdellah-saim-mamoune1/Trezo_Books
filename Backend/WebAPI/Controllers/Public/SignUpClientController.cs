using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.ClientDTO_s;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.PublicControllers
{
    [Route("api/public/sign-up[controller]")]
    [ApiController]
    public class SignUpClientController(IClientRegistrationService _Client) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> RegisterClient(ClientSignUpDto Form)
        {
            var result = await _Client.SignUpClientAsync(Form);
            if (result.Status == 200)
            {
                CreateCookies(result.Data!);
                return Ok(result);
            }
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);

        }

        private void CreateCookies(TokenResponseDto tokens)
        {

            Response.Cookies.Append("accessToken", tokens.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddMinutes(10)
            });

            Response.Cookies.Append("refreshToken", tokens.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });
        }
    }
}
