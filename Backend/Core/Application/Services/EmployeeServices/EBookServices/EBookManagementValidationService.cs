using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Application.Validators.EmployeeValidators.BookValidators;
using EcommerceBackend.Core.Application.Validators.SharedValidators;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.BookServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EBookServices
{
    public class EBookManagementValidationService
        (

       AppDbContext _db

        ) : IEBookManagementValidationService
    {
        public async Task<List<DValidationErorrs>?> ValidateAdd(DEAddBook searchForm)
        {
            List<DValidationErorrs> errors = new();
            var validator = new AddBookValidator();
            var result=await validator.ValidateAsync(searchForm);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
                return errors;
            }

        
             if(!await AuthorExistsByName(searchForm.Author))
                errors.Add(new DValidationErorrs { FieldId = "Author", Message = "author not found." });

             if(await BookExists(searchForm.Type, searchForm.Author, searchForm.Title))
                errors.Add(new DValidationErorrs { FieldId = "Book", Message = "Book already exists." });

               return errors.Count==0 ? null :errors;
        }


        public async Task<List<DValidationErorrs>?> ValidateUpdate(DEBookGetXUpdate book)
        {
            List<DValidationErorrs> errors = new();
            var validator = new BookValidator();
            var result = await validator.ValidateAsync(book);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            }

             if (!await BookTypeExistsById(book.TypeId))
                errors.Add(new DValidationErorrs { FieldId = "TypeId", Message = "book type not found." });
             if (!await AuthorExistsById(book.AuthorId))
                errors.Add(new DValidationErorrs { FieldId = "AuthorId", Message = "author not found." });
             if (!await BookExistsById(book.Id))
                errors.Add(new DValidationErorrs { FieldId = "Id", Message ="Book not found." });


            return errors.Count == 0 ? null : errors;
        }

        public async Task<List<DValidationErorrs>?> ValidateDelete(int Id)
        {
             if (!await BookExistsById(Id))
               return new List<DValidationErorrs> { new DValidationErorrs { FieldId = "Id", Message = "Book not found." }};

             if(await BookCopyExistsById(Id))
                return new List<DValidationErorrs> { new DValidationErorrs { FieldId = "Id", Message = "Book have copies." } };
            return null;
        }

        public List<DValidationErorrs>? ValidateGetPaginated(DPaginationForm Form)
        {
            List<DValidationErorrs> errors = new();
            var Validator = new PaginationFormValidator();

            var result = Validator.Validate(Form);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            }

            return errors.Count != 0 ? errors : null;
          
        }




        private async Task<bool> BookExists(string Type,string Author,string Name )
        {
            return await _db.Books.Include(b=>b.Author).Include(b=>b.BookType).AnyAsync(b => b.BookType!.Name.ToLower() == Type.ToLower() &&
            b.Author!.FullName.ToLower() == Author.ToLower() && b.Name.ToLower() == Name.Trim().ToLower());
        }

        private async Task<bool> BookCopyExistsById(int Id)
        {
            return await _db.BooksCopies.AnyAsync(b => b.BookId == Id);
        }

        private async Task<bool> BookTypeExistsById(int Id)
        {
            return await _db.BooksTypes.AnyAsync(b => b.Id == Id);
        }

        private async Task<bool> AuthorExistsById(int Id)
        {
            return await _db.Authors
                .AnyAsync(author => author.Id == Id);
        }

        private async Task<bool> AuthorExistsByName(string Name)
        {
            return await _db.Authors
                .AnyAsync(author => author.FullName.ToLower() == Name.ToLower());
        }

        private async Task<bool> BookExistsById(int Id)
        {
            return await _db.Books.AnyAsync(b => b.Id == Id);
        }

    }
}
