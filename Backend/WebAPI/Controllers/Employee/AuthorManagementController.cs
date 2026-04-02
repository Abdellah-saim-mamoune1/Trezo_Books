using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.EmployeeControllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/employee/author")]
    [ApiController]
    public class AuthorManagementController(IAuthorManagementService _Manage) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAuthorAsync(AuthorDto author)
        {
            var result = await _Manage.CreateAuthorAsync(author);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthorAsync(AuthorGetXUpdateDto author)
        {
            var result = await _Manage.UpdateAuthorAsync(author);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpDelete("{AuthorId}")]
        public async Task<IActionResult> DeleteAuthorAsync(int AuthorId)
        {
            var result = await _Manage.DeleteAuthorAsync(AuthorId);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpGet("{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetPaginatedAuthors(int PageSize, int PageNumber)
        {
            var Form = new PaginationFormDto { pageSize = PageSize, pageNumber = PageNumber };
            var result = await _Manage.GetPaginatedAuthorsAsync(Form);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpGet("by-id/{Id}")]
        public async Task<IActionResult> GetAuthorById(int Id)
        {
            var result = await _Manage.GetAuthorByIdAsync(Id);
            return Ok(result);
        }
    }
}