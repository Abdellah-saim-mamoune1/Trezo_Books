using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Application.Validators.EmployeeValidators.BookCopyValidators;
using EcommerceBackend.Core.Application.Validators.SharedValidators;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EBookCopyServicesInterfaces;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EBookCopyServices
{
    public class EBookCopyManagementValidationService
        (
       AppDbContext _db

        ): IEBookCopyManagementValidationService
    {


        public async Task<List<DValidationErorrs>?> ValidateAdd(DEBookCopy bookCopy)
        {
            List<DValidationErorrs> errors = new();
            var validator = new BookCopyValidator();
            var result =  validator.Validate(bookCopy);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            }

            if (!await BookExistsById(bookCopy.BookId))
                errors.Add(new DValidationErorrs { FieldId = "BookId", Message = "Book not found." });
            if(await ValidateBookAlreadyHasCopyByIdAsync(bookCopy.BookId))
                errors.Add(new DValidationErorrs { FieldId = "BookId", Message = "Book already has a copy." });

            return errors.Count == 0 ? null : errors;
        }


        public async Task<List<DValidationErorrs>?> ValidateUpdate(DBookCopyGetXUpdate bookCopy)
        {
            List<DValidationErorrs> errors = new();
            var validator = new BookCopyValidator();
            var result = await validator.ValidateAsync(bookCopy);

            if (!result.IsValid)
            {
                errors = result.Errors.Select(e => new DValidationErorrs { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            }

           
            if (!await BookExistsById(bookCopy.BookId))
                errors.Add(new DValidationErorrs { FieldId = "BookId", Message = "Book not found." });

            if(!await BookCopyExistsById(bookCopy.Id))
                errors.Add(new DValidationErorrs { FieldId = "Id", Message = "Book copy not found." });

            return errors.Count == 0 ? null : errors;
        }

        public async Task<List<DValidationErorrs>?> ValidateDelete(int Id)
        {
            List<DValidationErorrs> errors = new();
            if (await ValidateBookCopyIsOrdered(Id))
                errors.Add(new DValidationErorrs { FieldId = "Id.", Message = "Book copy is in orders." });
            if (!await BookCopyExistsById(Id))
                errors.Add( new DValidationErorrs { FieldId = "Id", Message = "Book copy not found." } );

            return errors.Count == 0 ? null : errors;
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

        private async Task<bool> BookExistsById(int Id)
        {
            return await _db.Books.AnyAsync(b => b.Id == Id);
        }

        private async Task<bool> ValidateBookAlreadyHasCopyByIdAsync(int Id)
        {
            return await _db.BooksCopies.AnyAsync(b => b.BookId == Id);
        }

        private async Task<bool> ValidateBookCopyIsOrdered(int Id)
        {
            return await _db.OrderItems.Include(o => o.order).AnyAsync(o => o.BookCopyId == Id);
        }

        private async Task<bool> BookCopyExistsById(int Id)
        {
            return await _db.BooksCopies.AnyAsync(b => b.Id == Id);
        }
    }
}
