using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces
{
    public interface IAuthorManagementValidationService
    {
        public Task<List<ValidationErorrsDto>?> ValidateAdd(AuthorDto author);
        public Task<List<ValidationErorrsDto>?> ValidateUpdate(AuthorGetXUpdateDto author);
        public Task<List<ValidationErorrsDto>?> ValidateDelete(int Id);
        public List<ValidationErorrsDto>? ValidateGetPaginated(PaginationFormDto Form);
    }
}