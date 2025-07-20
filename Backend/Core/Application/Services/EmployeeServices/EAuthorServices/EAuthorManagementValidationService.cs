using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Application.Validators.EmployeeValidators.AuthorValidators;
using EcommerceBackend.Core.Application.Validators.SharedValidators;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EAuthorServices
{
    public class EAuthorManagementValidationService(
        IEAuthorRepository _Repo,
        AppDbContext _db

       ) : IEAuthorManagementValidationService
    {
        public async Task<List<DValidationErorrs>?> ValidateAdd(DEAuthor author)
        {
            List<DValidationErorrs> errors = new();
            var Validator = new AuthorValidator();
            var result = await Validator.ValidateAsync(author);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            }
            if (await AuthorExistsByFullNameAndBirthDate(author.FullName))
            {
                errors.Add(new DValidationErorrs { FieldId = "Author", Message = "Author already exists." });
            }

            return errors.Count != 0 ? errors : null;
        }


        public async Task<List<DValidationErorrs>?> ValidateDelete(int AuthorId)
        {
            List<DValidationErorrs> errors = new();
            if (!await AuthorExistsById(AuthorId))
            {
                errors.Add(new DValidationErorrs { FieldId = "AuthorId", Message = "No author found." });
                return errors;
            }
            if (await AuthorBooksExists(AuthorId))
            {
                errors.Add(new DValidationErorrs { FieldId = "Author books", Message = "Cannot delete author because they have books." });
                return errors;
            }
            return errors.Count != 0 ? errors : null;
        }

        public async Task<List<DValidationErorrs>?> ValidateUpdate(DEAuthorGetXUpdate Author)
        {
            List<DValidationErorrs> errors = new();
            var Validator = new AuthorValidator();

            var result = await Validator.ValidateAsync(Author);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            }

            if (!await AuthorExistsById(Author.Id))
            {
                errors.Add(new DValidationErorrs { FieldId = "Id", Message = "author not found." });
            }

            return errors.Count != 0 ? errors : null;
        }


        public async Task<List<DValidationErorrs>?> ValidateGetPaginated(DPaginationForm Form)
        {
            List<DValidationErorrs> errors = new();
            var Validator = new PaginationFormValidator();

            var result = await Validator.ValidateAsync(Form);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            }

            return errors.Count != 0 ? errors : null;
        }


        private async Task<bool> AuthorExistsByFullNameAndBirthDate(string FullName)
        {
           
            return await _Repo.GetAllAuthorsQueryable()
                .AnyAsync(author => author.FullName.ToLower() == FullName.ToLower());

        }

        private async Task<bool> AuthorExistsById(int Id)
        {
            return await _Repo.GetAllAuthorsQueryable()
                .AnyAsync(author => author.Id == Id);
        }

        private async Task<bool> AuthorBooksExists(int Id)
        {
            return await _db.Books.AnyAsync(b => b.AuthorId == Id);
        }

    }
}
