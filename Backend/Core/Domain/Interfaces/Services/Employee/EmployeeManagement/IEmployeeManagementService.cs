using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.ClientDTO_s;
using EcommerceBackend.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EEmployeeManagementServicesInterfaces
{
    public interface IEmployeeManagementService
    {
        public Task<ApiResponseDto<TokenResponseDto?>> RegisterAsync(EmployeeSignUpDto SignUpInfos);
        public Task<ApiResponseDto<object?>> ResetPasswordAsync(ResetPasswordDto form, int Id);
        public Task<ApiResponseDto<object?>> DeleteAsync(int Id);
        public Task<ApiResponseDto<object?>> GetAllAsync();
        public Task<ApiResponseDto<object?>> GetByIdAsync(int Id);
        public Task<ApiResponseDto<object?>> UpdateAsync(PersonDto form, int Id);
    }
}
