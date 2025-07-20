using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.BookDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.BookServicesInterfaces
{
    public interface IEBookManagementService
    {
        public Task<DApiResponse<object?>> CreateBookAsync(string ISBN);
        public Task<DApiResponse<object?>> UpdateBookAsync(DEBookGetXUpdate book);
        public Task<DApiResponse<object?>> DeleteBookAsync(int BookId);
        public Task<DApiResponse<object?>> GetPaginatedBooksAsync(DPaginationForm Form);
        public Task<DApiResponse<object?>> GetBookByIdAsync(int Id);
        public Task<DApiResponse<object?>> GetBookByNameAsync(string Name);
    }
}
