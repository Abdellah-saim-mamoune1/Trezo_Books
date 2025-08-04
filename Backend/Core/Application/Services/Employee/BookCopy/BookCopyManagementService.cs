using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EBookCopyServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.UtilityClasses;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EBookCopyServices
{
    public class BookCopyManagementService
        (
    
        IBookCopyRepository _Repo,
        IBookCopyManagementValidationService _Validate

        ) : IBookCopyManagementService
    {


        public async Task<ApiResponseDto<object?>> CreateBookCopyAsync(BookCopyDto bookCopy)
        {
            var Errors = await _Validate.ValidateAdd(bookCopy);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }
            int BookId = await _Repo.Create(bookCopy);
            if (BookId == -1)
            {
                var ServerError = new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Server.", Message = "Internal server Error." } };
                return UApiResponder<object>.Fail("Internal server error.", ServerError, 500);
            }

            return UApiResponder<object>.Success(BookId, "Book copy was created successfully.");
        }


        public async Task<ApiResponseDto<object?>> UpdateBookCopyAsync(DBookCopyGetXUpdate bookCopy)
        {

            var Errors = await _Validate.ValidateUpdate(bookCopy);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            if (await _Repo.Update(bookCopy) == ValidationStatus.Fail)
            {
                var ServerError = new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Server.", Message = "Internal server Error." } };
                return UApiResponder<object>.Fail("Internal server error.", ServerError, 500);
            }

            return UApiResponder<object>.Success(null, "Book copy was updated successfully.");
        }

        public async Task<ApiResponseDto<object?>> DeleteBookCopyAsync(int BookCopyId)
        {
            var Errors = await _Validate.ValidateDelete(BookCopyId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            if (await _Repo.Delete(BookCopyId) == ValidationStatus.Fail)
            {
                var ServerError = new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Server.", Message = "Internal server Error." } };
                return UApiResponder<object>.Fail("Internal server error.", ServerError, 500);
            }

            return UApiResponder<object>.Success(null, "Book copy was deleted successfully.");
        }


        public async Task<ApiResponseDto<object?>> GetPaginatedBooksCopiesAsync(PaginationFormDto Form)
        {
            var Errors = _Validate.ValidateGetPaginated(Form);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var Authors = await _Repo.GetPaginatedBooksCopiesAsync(Form);

            return UApiResponder<object>.Success(Authors, "Books Copies were fetched successfully.");
        }


        public async Task<ApiResponseDto<object?>> GetBookCopyByIdAsync(int Id)
        {

            var Copy = await _Repo.GetBookCopyByIdAsync(Id);

            return UApiResponder<object>.Success(Copy,Copy==null?"Book copy not found." : "Book copy was fetched successfully.");
        }




    }
}
