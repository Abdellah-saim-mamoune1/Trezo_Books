using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CWishlistServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.WebAPI.Controllers.ClientControllers
{
    [Authorize(Roles ="Client")]
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistManagementController(ICWishlistManagementService _Manage) : ControllerBase
    {


        [HttpGet("GetClientWishlist/")]
        public async Task<IActionResult> GetClientWishlistAsync()
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int clientId = int.Parse(clientIdClaim!.Value);

            var result = await _Manage.GetClientWishlistItemAsync(clientId);

                return Ok(result);
        }



        [HttpPost("AddNewWishlistItem/")]
        public async Task<IActionResult> AddNewCartItemAsync( int BookCopyId)
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int clientId = int.Parse(clientIdClaim!.Value);

            var result = await _Manage.AddToListAsync(BookCopyId,clientId);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }



        [HttpDelete("DeleteWishlistItem/")]
        public async Task<IActionResult> DeleteCartItemAsync(int ItemId)
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int clientId = int.Parse(clientIdClaim!.Value);

            var result = await _Manage.DeleteListItemAsync(ItemId,clientId);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


    }
}
