using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.BookServicesInterfaces
{
    public interface IBookManagementValidationService
    {
        public Task<List<ValidationErorrsDto>?> ValidateAdd(AddBookDto searchForm);
        public Task<List<ValidationErorrsDto>?> ValidateUpdate(BookGetXUpdateDto book);
        public Task<List<ValidationErorrsDto>?> ValidateDelete(int Id);
        public List<ValidationErorrsDto>? ValidateGetPaginated(PaginationFormDto Form);
    }
}
