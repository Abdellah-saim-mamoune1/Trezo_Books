using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EOrdersServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.EmployeeControllers
{
    [Authorize(Roles = "Admin,Seller")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementByEmployeeController(IEOrdersManagementService _Manage) : ControllerBase
    {

        [HttpGet("GetOrders/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetOrdersAsync(int PageNumber,int PageSize)
        {
            var form = new DPaginationForm
            {
                pageNumber = PageNumber,
                pageSize = PageSize
            };
            var result = await _Manage.GetPaginatedOrderAsync(form);

            if (result.Status == 200)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("SetOrderStatus/{OrderId},{Status}")]
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
