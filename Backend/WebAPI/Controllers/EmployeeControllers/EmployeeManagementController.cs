using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EEmployeeManagementServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.WebAPI.Controllers.EmployeeControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeManagementController(IEEmployeeManagementService _EmployeeManagement) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterEmployee/")]
        public async Task<IActionResult> RegisterEmployee(DEEmployeeSignUp Form)
        {

            var result = await _EmployeeManagement.RegisterAsync(Form);
            if (result.Status == 200)
            {
                CreateCookies(result.Data!);
                return Ok(result);
            }
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteEmployee/{EmployeeId}")]
        public async Task<IActionResult> DeleteEmployee(int EmployeeId)
        {

            var result = await _EmployeeManagement.DeleteAsync(EmployeeId);
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("UpdateEmployeeInfo/")]
        public async Task<IActionResult> UpdateEmployee(DPerson form)
        {
            var EmployeeIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var Id = int.Parse(EmployeeIdClaim!.Value);

            var result = await _EmployeeManagement.UpdateAsync(form,Id);
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("ResetPassword/")]
        public async Task<IActionResult> ResetPassword(DResetPassword form)
        {
            var EmployeeIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var Id = int.Parse(EmployeeIdClaim!.Value);

            var result = await _EmployeeManagement.ResetPasswordAsync(form,Id);
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllEmployees/")]
        public async Task<IActionResult> GetAllEmployees()
        {

            var result = await _EmployeeManagement.GetAllAsync();
           
                return Ok(result);
            
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("GetEmployeeInfo/")]
        public async Task<IActionResult> GetEmployeeInfo()
        {
            var EmployeeIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var Id = int.Parse(EmployeeIdClaim!.Value);

            var result = await _EmployeeManagement.GetByIdAsync(Id);

            return Ok(result);

        }



        private void CreateCookies(DTokenResponse tokens)
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
