using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces;
using EcommerceBackend.DTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.UtilityClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.PublicControllers
{
    [Route("api/public/authentication")]
    [ApiController]
    public class AuthenticationController(IAuthService _AuthService) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
         
                var data = await _AuthService.Login(request);
            if (data.Status == 400)
                return BadRequest(data);

            else if (data.Status == 500)
                return StatusCode(500, data);

            CreateCookies(data.Data!);
                return Ok(UApiResponder<object>.Success(data!.Data!.Role));

        }

        [HttpPost("refresh-tokens/{Role}")]
        public async Task<IActionResult> RefreshToken(string Role)
        {

                if (!Request.Cookies.TryGetValue("refreshToken", out var RefreshToken))
                {
                    return Unauthorized(UApiResponder<object>.Fail("Invalid tokens", null, 401));
                }

                var data = await _AuthService.RefreshTokens(new RefreshTokenRequestDto
                {
                    RefreshToken = RefreshToken,
                    Role = Role
                });

                if (data.Data == null)
                {
                    return Unauthorized(data);
                }

                CreateCookies(data.Data!);
                  return Ok(UApiResponder<object>.Success(null));

           
        }


        [Authorize]
        [HttpDelete("cookies/")]
        public IActionResult DeleteCookies()
        {
            
            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete("refreshToken");

            return Ok();
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
