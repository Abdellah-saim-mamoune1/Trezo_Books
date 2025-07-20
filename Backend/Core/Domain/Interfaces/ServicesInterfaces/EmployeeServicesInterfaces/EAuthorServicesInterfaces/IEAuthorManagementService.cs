using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces
{
    public interface IEAuthorManagementService
    {
        public Task<DApiResponse<object?>> CreateAuthorAsync(DEAuthor author);
        public Task<DApiResponse<object?>> DeleteAuthorAsync(int AuthorId);
        public Task<DApiResponse<object?>> UpdateAuthorAsync(DEAuthorGetXUpdate author);
        public Task<DApiResponse<object?>> GetPaginatedAuthorAsync(DPaginationForm Form);
        public Task<DApiResponse<object?>> GetAuthorByIdAsync(int Id);
        public Task<DApiResponse<object?>> GetAuthorByNameAsync(string Name);
    }
}
