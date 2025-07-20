using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CCartServicesInterfaces;
using EcommerceBackend.DTO_s.CartDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.WebAPI.Controllers.ClientControllers
{
    [Authorize(Roles ="Client")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartManagementController(ICCartManagementService _Manage) : ControllerBase
    {

        [HttpPost("AddNewCartItem/{BookId}")]
        public async Task<IActionResult> AddNewCartItemAsync( int BookId)
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var clientId=int.Parse(clientIdClaim!.Value);
          
            var result = await _Manage.AddToCartAsync(BookId, clientId);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpPut("UpdateCartItem/")]
        public async Task<IActionResult> UpdateCartItemAsync(DCUpdateCartItem Item)
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var clientId = int.Parse(clientIdClaim!.Value);
            var result = await _Manage.UpdateAsync(Item, clientId);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpDelete("DeleteCartItem/{ItemId}")]
        public async Task<IActionResult> DeleteCartItemAsync(int ItemId)
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var clientId = int.Parse(clientIdClaim!.Value);
            var result = await _Manage.DeleteAsync(ItemId, clientId);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpGet("GetCartItems/")]
        public async Task<IActionResult> GetCartItemAsync()
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var clientId = int.Parse(clientIdClaim!.Value);

            var result = await _Manage.GetAsync(clientId);

            if (result.Status == 200)
                return Ok(result);

                return BadRequest(result);

        }



    }
}
