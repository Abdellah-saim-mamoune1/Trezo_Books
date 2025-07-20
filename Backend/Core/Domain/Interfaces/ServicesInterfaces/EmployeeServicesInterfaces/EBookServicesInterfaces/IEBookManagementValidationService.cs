using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.BookDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.BookServicesInterfaces
{
    public interface IEBookManagementValidationService
    {
        public Task<List<DValidationErorrs>?> ValidateAdd(DEAddBook searchForm);
        public Task<List<DValidationErorrs>?> ValidateUpdate(DEBookGetXUpdate book);
        public Task<List<DValidationErorrs>?> ValidateDelete(int Id);
        public List<DValidationErorrs>? ValidateGetPaginated(DPaginationForm Form);
    }
}
