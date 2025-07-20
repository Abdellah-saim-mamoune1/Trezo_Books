using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.ClientDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces
{
    public interface ICClientManagementValidatorsService
    {
        public  Task<List<DValidationErorrs>?> ValidateSignUp(DCClientSignUp Form);
        public  Task<List<DValidationErorrs>?> ValidateGet(int ClientId);
        public  Task<List<DValidationErorrs>?> ValidateResetPassword(DResetPassword form, int ClientId);
        public  Task<List<DValidationErorrs>?> ValidateUpdate(DCUpdateClientProfileInfo Form, int Id);
    }
}
