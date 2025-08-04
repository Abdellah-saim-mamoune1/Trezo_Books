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
    [Route("api/employee/")]
    [ApiController]
    public class EmployeeManagementController(IEmployeeManagementService _EmployeeManagement) : ControllerBase
    {

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterEmployee(EmployeeSignUpDto Form)
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
        [HttpDelete("{EmployeeId}")]
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
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(PersonDto form)
        {
           
            var result = await _EmployeeManagement.UpdateAsync(form, GetUserId());
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [Authorize(Roles = "Admin,Seller")]
        [HttpPut("password/")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto form)
        {
       
            var result = await _EmployeeManagement.ResetPasswordAsync(form,GetUserId());
            if (result.Status == 200)
            {
                return Ok(result);
            }
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("employees/")]
        public async Task<IActionResult> GetAllEmployees()
        {

            var result = await _EmployeeManagement.GetAllAsync();
           
                return Ok(result);
            
        }

        [Authorize(Roles = "Admin,Seller")]
        [HttpGet("employee/")]
        public async Task<IActionResult> GetEmployeeInfo()
        {
           
            var result = await _EmployeeManagement.GetByIdAsync(GetUserId());

            return Ok(result);

        }


        private int GetUserId()
        {
            var Id = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(Id!.Value);
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
