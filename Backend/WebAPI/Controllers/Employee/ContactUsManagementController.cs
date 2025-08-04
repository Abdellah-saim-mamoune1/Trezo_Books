using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.ContactUsManagementServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.EmployeeControllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/employee/contact")]
    [ApiController]
    public class ContactUsManagementController(IContactUsMessagesManagementService _Manage) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpDelete("{MessageId}")]
        public async Task<IActionResult> DeleteMessage(int MessageId)
        {

            var result = await _Manage.DeleteAsync(MessageId);
            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);

        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {

            var result = await _Manage.GetAsync();
    
                return Ok(result);
        }
    }
}
