using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces
{
    public interface IEAuthorManagementValidationService
    {
        public Task<List<DValidationErorrs>?> ValidateAdd(DEAuthor author);
        public Task<List<DValidationErorrs>?> ValidateDelete(int AuthorId);
        public Task<List<DValidationErorrs>?> ValidateUpdate(DEAuthorGetXUpdate Author);
        public Task<List<DValidationErorrs>?> ValidateGetPaginated(DPaginationForm Form);
    }
}
