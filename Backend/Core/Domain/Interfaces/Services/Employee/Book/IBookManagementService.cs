using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.BookServicesInterfaces
{
    public interface IBookManagementService
    {
        public Task<ApiResponseDto<object?>> CreateBookAsync(string ISBN);
        public Task<ApiResponseDto<object?>> UpdateBookAsync(BookGetXUpdateDto book);
        public Task<ApiResponseDto<object?>> DeleteBookAsync(int BookId);
        public Task<ApiResponseDto<object?>> GetPaginatedBooksAsync(PaginationFormDto Form);
        public Task<ApiResponseDto<object?>> GetBookByIdAsync(int Id);
        public Task<ApiResponseDto<object?>> GetBookByNameAsync(string Name);
    }
}
