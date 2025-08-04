using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EBookCopyServicesInterfaces
{
    public interface IBookCopyManagementService
    {
        public Task<ApiResponseDto<object?>> CreateBookCopyAsync(BookCopyDto bookCopy);
        public Task<ApiResponseDto<object?>> UpdateBookCopyAsync(DBookCopyGetXUpdate bookCopy);
        public Task<ApiResponseDto<object?>> DeleteBookCopyAsync(int BookCopyId);
        public Task<ApiResponseDto<object?>> GetPaginatedBooksCopiesAsync(PaginationFormDto Form);
        public Task<ApiResponseDto<object?>> GetBookCopyByIdAsync(int Id);
    }
}
