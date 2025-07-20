using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EBookCopyServicesInterfaces
{
    public interface IEBookCopyManagementValidationService
    {
        public Task<List<DValidationErorrs>?> ValidateAdd(DEBookCopy bookCopy);
        public Task<List<DValidationErorrs>?> ValidateUpdate(DBookCopyGetXUpdate bookCopy);
        public Task<List<DValidationErorrs>?> ValidateDelete(int Id);
        public List<DValidationErorrs>? ValidateGetPaginated(DPaginationForm Form);
    }
}
