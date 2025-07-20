using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.ClientDTO_s;
using EcommerceBackend.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EEmployeeManagementServicesInterfaces
{
    public interface IEEmployeeManagementService
    {
        public Task<DApiResponse<DTokenResponse?>> RegisterAsync(DEEmployeeSignUp SignUpInfos);
        public Task<DApiResponse<object?>> ResetPasswordAsync(DResetPassword form, int Id);
        public Task<DApiResponse<object?>> DeleteAsync(int Id);
        public Task<DApiResponse<object?>> GetAllAsync();
        public Task<DApiResponse<object?>> GetByIdAsync(int Id);
        public Task<DApiResponse<object?>> UpdateAsync(DPerson form, int Id);
    }
}
