using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IEmployeeRepository
    {
        public Task<TokenResponseDto?> RegisterAsync(EmployeeSignUpDto SignUpInfos);
        public Task<bool> ResetPasswordAsync(ResetPasswordDto data, int ClientId);
        public Task<List<GetEmployeesDto>> GetAllAsync();
        public Task<GetEmployeesDto> GetByIdAsync(int Id);
        public Task<bool> UpdateAsync(PersonDto form, int Id);
        public Task<bool> DeleteAsync(int Id);
    }
}
