using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.WebAPI.Controllers.EmployeeControllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorManagementController(IEAuthorManagementService _Manage) : ControllerBase
    {
        [HttpPost("AddNewAuthor/{Name}")]
        public async Task<IActionResult> AddNewAuthorAsync(string Name)
        {
            var author = new DEAuthor
            {
                FullName = Name
            };

            var result = await _Manage.CreateAuthorAsync(author);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpPut("UpdateAuthor/{Id},{Name}")]
        public async Task<IActionResult> UpdateAuthorAsync(int Id,string Name)
        {
            var author = new DEAuthorGetXUpdate
            {
                Id = Id,
                FullName = Name
            };
            var result = await _Manage.UpdateAuthorAsync(author);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpDelete("DeleteAuthor/{AuthorId}")]
        public async Task<IActionResult> DeleteAuthorAsync(int AuthorId)
        {

            var result = await _Manage.DeleteAuthorAsync(AuthorId);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpGet("GetPaginatedAuthors/{PageNumber},{PageSize}")]
        public async Task<IActionResult> GetPaginatedAuthors(int PageSize,int PageNumber)
        {
            var Form = new DPaginationForm { pageSize = PageSize, pageNumber = PageNumber };
            var result = await _Manage.GetPaginatedAuthorAsync(Form);

            if (result.Status == 200)
                return Ok(result);

            else if (result.Status == 400)
                return BadRequest(result);

            return StatusCode(500, result);
        }

        [HttpGet("GetAuthorById/{Id}")]
        public async Task<IActionResult> GetAuthorById(int Id)
        {
            
            var result = await _Manage.GetAuthorByIdAsync(Id);
                return Ok(result);

        }

        [HttpGet("GetAuthorByName/{Name}")]
        public async Task<IActionResult> GetAuthorByName(string Name)
        {

            var result = await _Manage.GetAuthorByNameAsync(Name);
            return Ok(result);

        }


    }
}
