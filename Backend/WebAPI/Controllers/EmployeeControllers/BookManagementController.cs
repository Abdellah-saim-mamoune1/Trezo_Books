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
    [Route("api/[controller]")]
    [ApiController]
    public class BookManagementController(IEBookManagementService _Manage, HttpClient http, AppDbContext db) : ControllerBase
    {
       
       [HttpPost("AddNewBook/{ISBN}")]
        public async Task<IActionResult> AddNewBookAsync(string ISBN)
        {

            var result = await _Manage.CreateBookAsync(ISBN);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [HttpPost("AddGoogleBook/")]
        public async Task<IActionResult> AddGoogleBookAsync(string Type)
        {

            var g = new GoogleBooksImporterService(http,db);

            await g.ImportBooksAsync(Type);
            return Ok();
        }

        [HttpPut("UpdateBook/")]
        public async Task<IActionResult> UpdateBookAsync(DEBookGetXUpdate book)
        {

            var result = await _Manage.UpdateBookAsync(book);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpDelete("DeleteBook/{BookId}")]
        public async Task<IActionResult> DeleteBookAsync(int BookId)
        {

            var result = await _Manage.DeleteBookAsync(BookId);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [HttpGet("GetPaginatedBooks/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetPaginatedBooks(int PageSize, int PageNumber)
        {
            var Form = new DPaginationForm { pageSize = PageSize, pageNumber = PageNumber };
            var result = await _Manage.GetPaginatedBooksAsync(Form);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }


        [HttpGet("GetBookById/{Id}")]
        public async Task<IActionResult> GetBookById(int Id)
        {

            var result = await _Manage.GetBookByIdAsync(Id);

                return Ok(result);

        }

        [HttpGet("GetBookByName/{Name}")]
        public async Task<IActionResult> GetBookByName(string Name)
        {

            var result = await _Manage.GetBookByNameAsync(Name);

            return Ok(result);

        }
    }
}
