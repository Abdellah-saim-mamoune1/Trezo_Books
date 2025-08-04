using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CCartServicesInterfaces;
using EcommerceBackend.DTO_s.CartDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.WebAPI.Controllers.ClientControllers
{
    [Authorize(Roles ="Client")]
    [Route("api/client/cart")]
    [ApiController]
    public class CartManagementController(ICartManagementService _Manage) : ControllerBase
    {

        [HttpPost("{BookId}")]
        public async Task<IActionResult> AddNewCartItemAsync( int BookId)
        {
            
            var result = await _Manage.AddToCartAsync(BookId, GetUserId());

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItemAsync(UpdateCartItemDto Item)
        {
           
            var result = await _Manage.UpdateAsync(Item, GetUserId());

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpDelete("{ItemId}")]
        public async Task<IActionResult> DeleteCartItemAsync(int ItemId)
        {
        
            var result = await _Manage.DeleteAsync(ItemId, GetUserId());

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItemAsync()
        {
      
            var result = await _Manage.GetAsync(GetUserId());

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
