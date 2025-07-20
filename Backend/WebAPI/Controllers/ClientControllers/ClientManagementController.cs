using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.WebAPI.Controllers.ClientControllers
{
    [Authorize(Roles = "Client")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientManagementController : ControllerBase
    {
        ICClientRegistrationService _clientAccountService;
        public ClientManagementController(ICClientRegistrationService clientAccountService)
        {
             _clientAccountService= clientAccountService;
        }
         

        [HttpGet("GetClientInfo/")]
        public async Task<IActionResult> GetClientInfo()
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var clientId = int.Parse(clientIdClaim!.Value);

            var result = await _clientAccountService.GetClientInfoAsync(clientId);
            if (result.Status == 200)
                return Ok(result);
            
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);

        }

        [HttpPatch("UpdateClientProfile/")]
        public async Task<IActionResult> UpdateClientProfile(DCUpdateClientProfileInfo Form)
        {

            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var clientId = int.Parse(clientIdClaim!.Value);

            var result = await _clientAccountService.UpdateClientProfileAsync(Form,clientId);
            if (result.Status == 200)
                return Ok(result);
            
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);

        }

        [HttpPut("ResetPassword/")]
        public async Task<IActionResult> ResetPassword(DResetPassword Form)
        {

            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var clientId = int.Parse(clientIdClaim!.Value);

            var result = await _clientAccountService.ResetPasswordAsync(Form, clientId);
            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);

        }




    }
}
