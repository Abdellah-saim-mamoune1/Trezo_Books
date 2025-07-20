using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.ContactUsManagementServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.PublicControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController(IEContactUsMessagesManagementService _Manage) : ControllerBase
    {

        [HttpPost("SendMessage/")]
        public async Task<IActionResult> SendMessage(DEContactUsSet Form)
        {

            var result = await _Manage.CreateAsync(Form);
            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);

        }
    }
}
