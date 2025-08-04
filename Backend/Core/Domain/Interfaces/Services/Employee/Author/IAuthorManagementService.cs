using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces
{
    public interface IAuthorManagementService
    {
        public Task<ApiResponseDto<object?>> CreateAuthorAsync(AuthorDto author);
        public Task<ApiResponseDto<object?>> DeleteAuthorAsync(int AuthorId);
        public Task<ApiResponseDto<object?>> UpdateAuthorAsync(AuthorGetXUpdateDto author);
        public Task<ApiResponseDto<object?>> GetPaginatedAuthorAsync(PaginationFormDto Form);
        public Task<ApiResponseDto<object?>> GetAuthorByIdAsync(int Id);
        public Task<ApiResponseDto<object?>> GetAuthorByNameAsync(string Name);
    }
}
