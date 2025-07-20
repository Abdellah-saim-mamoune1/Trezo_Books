using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EBookCopyServicesInterfaces
{
    public interface IEBookCopyManagementService
    {
        public Task<DApiResponse<object?>> CreateBookCopyAsync(DEBookCopy bookCopy);
        public Task<DApiResponse<object?>> UpdateBookCopyAsync(DBookCopyGetXUpdate bookCopy);
        public Task<DApiResponse<object?>> DeleteBookCopyAsync(int BookCopyId);
        public Task<DApiResponse<object?>> GetPaginatedBooksCopiesAsync(DPaginationForm Form);
        public Task<DApiResponse<object?>> GetBookCopyByIdAsync(int Id);
    }
}
