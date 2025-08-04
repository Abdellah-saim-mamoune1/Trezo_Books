using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.ClientDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces
{
    public interface IClientManagementValidatorsService
    {
        public  Task<List<ValidationErorrsDto>?> ValidateSignUp(ClientSignUpDto Form);
        public  Task<List<ValidationErorrsDto>?> ValidateGet(int ClientId);
        public  Task<List<ValidationErorrsDto>?> ValidateResetPassword(ResetPasswordDto form, int ClientId);
        public  Task<List<ValidationErorrsDto>?> ValidateUpdate(UpdateClientProfileInfoDto Form, int Id);
    }
}
