using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.WebAPI.Controllers.ClientControllers
{
    [Authorize(Roles = "Client")]
    [Route("api/client/manage")]
    [ApiController]
    public class ClientManagementController : ControllerBase
    {
        IClientRegistrationService _clientAccountService;
        public ClientManagementController(IClientRegistrationService clientAccountService)
        {
             _clientAccountService= clientAccountService;
        }
         

        [HttpGet]
        public async Task<IActionResult> GetClientInfo()
        {
            
            var result = await _clientAccountService.GetClientInfoAsync(GetUserId());
            if (result.Status == 200)
                return Ok(result);
            
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);

        }

        [HttpPatch]
        public async Task<IActionResult> UpdateClientProfile(UpdateClientProfileInfoDto Form)
        {

            var result = await _clientAccountService.UpdateClientProfileAsync(Form, GetUserId());
            if (result.Status == 200)
                return Ok(result);
            
            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);

        }

        [HttpPut("password/")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto Form)
        {

            var result = await _clientAccountService.ResetPasswordAsync(Form, GetUserId());
            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);

        }

        private int GetUserId()
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(clientIdClaim!.Value);
        }


    }
}
