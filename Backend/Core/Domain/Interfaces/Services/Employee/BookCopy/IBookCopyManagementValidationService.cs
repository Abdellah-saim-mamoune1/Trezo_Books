using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EBookCopyServicesInterfaces
{
    public interface IBookCopyManagementValidationService
    {
        public Task<List<ValidationErorrsDto>?> ValidateAdd(BookCopyDto bookCopy);
        public Task<List<ValidationErorrsDto>?> ValidateUpdate(DBookCopyGetXUpdate bookCopy);
        public Task<List<ValidationErorrsDto>?> ValidateDelete(int Id);
        public List<ValidationErorrsDto>? ValidateGetPaginated(PaginationFormDto Form);
    }
}
