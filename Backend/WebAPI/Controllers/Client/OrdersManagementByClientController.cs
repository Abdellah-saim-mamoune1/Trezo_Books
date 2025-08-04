using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.COrderServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.WebAPI.Controllers.ClientControllers
{
    [Authorize(Roles = "Client")]
    [Route("api/client/orders")]
    [ApiController]
    public class OrdersManagementByClientController(IOrderManagementService _Manage) : ControllerBase
    {


        [HttpPost]
        public async Task<IActionResult> AddOrderAsync(AddOrderDto form)
        {
           
            var result = await _Manage.CreateOrderAsync(form, GetUserId());

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [HttpGet("{pageNumber},{pageSize}")]
        public async Task<IActionResult> GetOrdersAsync(int pageNumber,int pageSize)
        {
            
            var form = new PaginationFormDto
            {
                pageSize = pageSize,
                pageNumber = pageNumber
            };
            var result = await _Manage.GetPaginatedOrderAsync(form, GetUserId());

            if (result.Status == 200)
                return Ok(result);

                return BadRequest(result);

        }


        private int GetUserId()
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(clientIdClaim!.Value);
        }



    }
}
