using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CBookCopyServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.PublicControllers
{
    [Route("api/public/book-copy")]
    [ApiController]
    public class GetBooksCopiesController(IBooksCopiesGetService _Get) : ControllerBase
    {


        [HttpGet("books-per-type/")]
        public async Task<IActionResult> GetInitialBooksCopiesData()
        {
            var Data = await _Get.GetInitialBooksCopiesDataPerType();
            return Ok(Data);
        }


        [HttpGet("recommended/")]
        public async Task<IActionResult> GetInitialRecommendedBooksCopiesData()
        {
            var Data = await _Get.GetInitialRecommendedBooksCopiesData();
            return Ok(Data);
        }


        [HttpGet("top-rated/")]
        public async Task<IActionResult> GetTopRatedBooksCopiesData()
        {
            var Data = await _Get.GetInitialTopRatedBooksCopiesData();
            return Ok(Data);
        }


        [HttpGet("best-sellers/")]
        public async Task<IActionResult> GetInitialBestSellersBooksCopiesData()
        {
            var Data = await _Get.GetInitialBestSellersBooksCopiesData();
            return Ok(Data);
        }

        
        [HttpGet("new-releases/")]
        public async Task<IActionResult> GetInitialNewReleasedBooksCopiesData()
        {
            var Data = await _Get.GetInitialNewReleasedBooksCopiesData();
            return Ok(Data);
        }


        [HttpGet("by-type/{Type},{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetInitialBooksCopiesDataByType(string Type,int PageNumber,int PageSize)
        {
            var pagination = new GetPaginatedBooksTypesDto
            {
                Type = Type,
                pageSize = PageSize,
                pageNumber = PageNumber,
            };
            var Data = await _Get.GetInitialBooksCopiesDataByType(pagination);
            if (Data.Status == 400)
                return BadRequest(Data);

            return Ok(Data);
        }


        [HttpGet("types/")]
        public async Task<IActionResult> GetBooksTypes()
        {
            var Data = await _Get.GetBooksTypes();
            return Ok(Data);
        }


        [HttpGet("by-id/{Id}")]
        public async Task<IActionResult> GetBookCopyInfoAsync(int Id)
        {
            var Data = await _Get.GetBookCopyInfoById(Id);
            if(Data.Status==200)
            return Ok(Data);

            return BadRequest(Data);
        }


        [HttpGet("by-name/{Name}")]
        public async Task<IActionResult> GetBooksCopiesInfoByNameAsync(string Name)
        {
            var Data = await _Get.GetBooksCopiesInfoByName(Name);
            if (Data.Status == 200)
                return Ok(Data);

            return BadRequest(Data);
        }

    }
}
