using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EOrdersServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.EmployeeControllers
{
    [Authorize(Roles = "Admin,Seller")]
    [Route("api/employee/order")]
    [ApiController]
    public class OrderManagementByEmployeeController(IOrdersManagementService _Manage) : ControllerBase
    {

        [HttpGet("{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetOrdersAsync(int PageNumber,int PageSize)
        {
            var form = new PaginationFormDto
            {
                pageNumber = PageNumber,
                pageSize = PageSize
            };
            var result = await _Manage.GetPaginatedOrderAsync(form);

            if (result.Status == 200)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("set-status/{OrderId},{Status}")]
        public async Task<IActionResult> SetOrderStatusAsync(int OrderId,string Status)
        {
            var result = await _Manage.SetOrderStatusAsync(OrderId, Status);

            if (result.Status == 200)
                return Ok(result);
            else if(result.Status ==400)
                return BadRequest(result);

            return StatusCode(500, result);

        }

    }
}
