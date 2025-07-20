using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CBookCopyServicesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.PublicControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetBooksCopiesController(ICGetBooksCopiesService _Get) : ControllerBase
    {


     

        [HttpGet("GetInitialBooksCopiesDataPerType/")]
        public async Task<IActionResult> GetInitialBooksCopiesData()
        {
            var Data = await _Get.GetInitialBooksCopiesDataPerType();
            return Ok(Data);
        }


        [HttpGet("GetInitialRecommendedBooksCopiesData/")]
        public async Task<IActionResult> GetInitialRecommendedBooksCopiesData()
        {
            var Data = await _Get.GetInitialRecommendedBooksCopiesData();
            return Ok(Data);
        }

        [HttpGet("GetTopRatedBooksCopiesData/")]
        public async Task<IActionResult> GetTopRatedBooksCopiesData()
        {
            var Data = await _Get.GetInitialTopRatedBooksCopiesData();
            return Ok(Data);
        }


        [HttpGet("GetInitialBestSellersBooksCopiesData/")]
        public async Task<IActionResult> GetInitialBestSellersBooksCopiesData()
        {
            var Data = await _Get.GetInitialBestSellersBooksCopiesData();
            return Ok(Data);
        }

        
        [HttpGet("GetInitialNewReleasedBooksCopiesData/")]
        public async Task<IActionResult> GetInitialNewReleasedBooksCopiesData()
        {
            var Data = await _Get.GetInitialNewReleasedBooksCopiesData();
            return Ok(Data);
        }

        [HttpGet("GetPaginatedInitialBooksCopiesDataByType/{Type},{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetInitialBooksCopiesDataByType(string Type,int PageNumber,int PageSize)
        {
            var pagination = new DCGetPaginatedBooksTypes
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

        [HttpGet("GetBooksTypes/")]
        public async Task<IActionResult> GetBooksTypes()
        {
            var Data = await _Get.GetBooksTypes();
            return Ok(Data);
        }

        [HttpGet("GetBookCopyInfo/{Id}")]
        public async Task<IActionResult> GetBookCopyInfoAsync(int Id)
        {
            var Data = await _Get.GetBookCopyInfoById(Id);
            if(Data.Status==200)
            return Ok(Data);

            return BadRequest(Data);
        }

        [HttpGet("GetBooksCopiesInfoByName/{Name}")]
        public async Task<IActionResult> GetBooksCopiesInfoByNameAsync(string Name)
        {
            var Data = await _Get.GetBooksCopiesInfoByName(Name);
            if (Data.Status == 200)
                return Ok(Data);

            return BadRequest(Data);
        }



    }
}
