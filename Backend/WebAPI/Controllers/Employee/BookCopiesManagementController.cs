using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EBookCopyServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.EmployeeControllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/employee/book-copy")]
    [ApiController]
    public class BookCopiesManagementController(IBookCopyManagementService _Manage) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddNewBookCopyAsync( BookCopyDto BookCopy)
        {

            var result = await _Manage.CreateBookCopyAsync(BookCopy);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookCopyAsync( DBookCopyGetXUpdate BookCopy)
        {

            var result = await _Manage.UpdateBookCopyAsync(BookCopy);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpDelete("{BookCopyId}")]
        public async Task<IActionResult> DeleteBookCopyAsync(int BookCopyId)
        {

            var result = await _Manage.DeleteBookCopyAsync(BookCopyId);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpGet("{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetPaginatedBooksCopies(int PageNumber , int PageSize)
        {
            var Form = new PaginationFormDto { pageSize = PageSize, pageNumber = PageNumber };
            var result = await _Manage.GetPaginatedBooksCopiesAsync(Form);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [HttpGet("by-id/{Id}")]
        public async Task<IActionResult> GetBookCopyById(int Id)
        {
          
            var result = await _Manage.GetBookCopyByIdAsync(Id);

                return Ok(result);

        }

    }
}
