using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.StatisticsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.EmployeeControllers
{
    [Authorize(Roles = "Admin,Seller")]
    [Route("api/[controller]")]
    [ApiController]
    public class GetDashBoardStatisticsController(IEStatisticsService _Get) : ControllerBase
    {
        [HttpGet("GetTotals/")]
        public async Task<IActionResult> GetTotals()
        {
            var Data = await _Get.GetTotalsAsync();
            return Ok(Data);
        }

        [HttpGet("GetResentOrders/")]
        public async Task<IActionResult> GetResentOrders()
        {
            var Data = await _Get.GetResentOrdersAsync();
            return Ok(Data);
        }

        [HttpGet("GetNewClients/")]
        public async Task<IActionResult> GetNewClients()
        {
            var Data = await _Get.GetNewClientsAsync();
            return Ok(Data);
        }

        [HttpGet("GetAllClients/")]
        public async Task<IActionResult> GetAllClients()
        {
            var Data = await _Get.GetAllClientsAsync();
            return Ok(Data);
        }
    }
}
