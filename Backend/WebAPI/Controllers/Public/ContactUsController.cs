using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.ContactUsManagementServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.PublicControllers
{
    [Route("api/public/contact")]
    [ApiController]
    public class ContactUsController(IContactUsMessagesManagementService _Manage) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> SendMessage(ContactUsSetDto Form)
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
