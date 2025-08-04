using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CWishlistServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.WebAPI.Controllers.ClientControllers
{
    [Authorize(Roles ="Client")]
    [Route("api/client/whish-list")]
    [ApiController]
    public class WishlistManagementController(IWishlistManagementService _Manage) : ControllerBase
    {


        [HttpGet]
        public async Task<IActionResult> GetClientWishlistAsync()
        {
           
            var result = await _Manage.GetClientWishlistItemAsync(GetUserId());

                return Ok(result);
        }



        [HttpPost("{BookCopyId}")]
        public async Task<IActionResult> AddNewCartItemAsync( int BookCopyId)
        {
       
            var result = await _Manage.AddToListAsync(BookCopyId,GetUserId());

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }



        [HttpDelete("{ItemId}")]
        public async Task<IActionResult> DeleteCartItemAsync(int ItemId)
        {
       
            var result = await _Manage.DeleteListItemAsync(ItemId,GetUserId());

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        private int GetUserId()
        {
            var clientIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(clientIdClaim!.Value);
        }

    }
}
