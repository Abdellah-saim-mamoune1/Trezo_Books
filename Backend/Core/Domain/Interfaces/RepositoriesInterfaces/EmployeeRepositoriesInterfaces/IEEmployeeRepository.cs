using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces
{
    public interface IEEmployeeRepository
    {
        public Task<DTokenResponse?> RegisterAsync(DEEmployeeSignUp SignUpInfos);
        public Task<bool> ResetPasswordAsync(DResetPassword data, int ClientId);
        public Task<List<DEGetEmployees>> GetAllAsync();
        public Task<DEGetEmployees> GetByIdAsync(int Id);
        public Task<bool> UpdateAsync(DPerson form, int Id);
        public Task<bool> DeleteAsync(int Id);
    }
}
