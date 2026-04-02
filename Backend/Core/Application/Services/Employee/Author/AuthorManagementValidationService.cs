using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Application.Validators.EmployeeValidators.AuthorValidators;
using EcommerceBackend.Core.Application.Validators.SharedValidators;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.AuthorServices
{
    public class AuthorManagementValidationService
        (
       AppDbContext _db
        ) : IAuthorManagementValidationService
    {
        public async Task<List<ValidationErorrsDto>?> ValidateAdd(AuthorDto author)
        {
            List<ValidationErorrsDto> errors = new();
            var validator = new AuthorValidator();
            var result = validator.Validate(author);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new ValidationErorrsDto { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            }

            if (await AuthorExistsByName(author.FullName))
                errors.Add(new ValidationErorrsDto { FieldId = "FullName", Message = "Author already exists." });

            return errors.Count == 0 ? null : errors;
        }

        public async Task<List<ValidationErorrsDto>?> ValidateUpdate(AuthorGetXUpdateDto author)
        {
            List<ValidationErorrsDto> errors = new();
            var validator = new AuthorValidator();
            var result = validator.Validate(author);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new ValidationErorrsDto { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            }

            if (!await AuthorExistsById(author.Id))
                errors.Add(new ValidationErorrsDto { FieldId = "Id", Message = "Author not found." });

            if (await AuthorExistsByNameAndNotId(author.FullName, author.Id))
                errors.Add(new ValidationErorrsDto { FieldId = "FullName", Message = "Author name already exists." });

            return errors.Count == 0 ? null : errors;
        }

        public async Task<List<ValidationErorrsDto>?> ValidateDelete(int Id)
        {
            if (!await AuthorExistsById(Id))
                return new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Id", Message = "Author not found." } };

            if (await AuthorHasBooks(Id))
                return new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Id", Message = "Author has books and cannot be deleted." } };

            return null;
        }

        public List<ValidationErorrsDto>? ValidateGetPaginated(PaginationFormDto Form)
        {
            List<ValidationErorrsDto> errors = new();
            var Validator = new PaginationFormValidator();

            var result = Validator.Validate(Form);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new ValidationErorrsDto { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            }

            return errors.Count != 0 ? errors : null;
        }

        private async Task<bool> AuthorExistsById(int Id)
        {
            return await _db.Authors.AnyAsync(a => a.Id == Id);
        }

        private async Task<bool> AuthorExistsByName(string Name)
        {
            return await _db.Authors.AnyAsync(a => a.FullName.ToLower() == Name.ToLower());
        }

        private async Task<bool> AuthorExistsByNameAndNotId(string Name, int Id)
        {
            return await _db.Authors.AnyAsync(a => a.FullName.ToLower() == Name.ToLower() && a.Id != Id);
        }

        private async Task<bool> AuthorHasBooks(int Id)
        {
            return await _db.Books.AnyAsync(b => b.AuthorId == Id);
        }
    }
}