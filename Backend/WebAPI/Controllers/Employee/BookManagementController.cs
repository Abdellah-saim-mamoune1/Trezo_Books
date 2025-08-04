using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.Services.EmployeeServices;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.BookServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.EmployeeControllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/employee/book")]
    [ApiController]
    public class BookManagementController(IBookManagementService _Manage, HttpClient http, AppDbContext db) : ControllerBase
    {
       
       [HttpPost("{ISBN}")]
        public async Task<IActionResult> AddNewBookAsync(string ISBN)
        {

            var result = await _Manage.CreateBookAsync(ISBN);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [HttpPost("google-book/")]
        public async Task<IActionResult> AddGoogleBookAsync(string Type)
        {

            var g = new GoogleBooksImporterService(http,db);

            await g.ImportBooksAsync(Type);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookAsync(BookGetXUpdateDto book)
        {

            var result = await _Manage.UpdateBookAsync(book);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpDelete("{BookId}")]
        public async Task<IActionResult> DeleteBookAsync(int BookId)
        {

            var result = await _Manage.DeleteBookAsync(BookId);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [HttpGet("{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetPaginatedBooks(int PageSize, int PageNumber)
        {
            var Form = new PaginationFormDto { pageSize = PageSize, pageNumber = PageNumber };
            var result = await _Manage.GetPaginatedBooksAsync(Form);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [HttpGet("by-id/{Id}")]
        public async Task<IActionResult> GetBookById(int Id)
        {

            var result = await _Manage.GetBookByIdAsync(Id);

                return Ok(result);

        }

        [HttpGet("by-name/{Name}")]
        public async Task<IActionResult> GetBookByName(string Name)
        {

            var result = await _Manage.GetBookByNameAsync(Name);

            return Ok(result);

        }
    }
}
