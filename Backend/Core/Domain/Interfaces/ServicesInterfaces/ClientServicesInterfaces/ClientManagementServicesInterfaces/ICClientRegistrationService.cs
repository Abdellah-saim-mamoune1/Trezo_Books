using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.ClientDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces
{
    public interface ICClientRegistrationService
    {
        public Task<DApiResponse<DTokenResponse?>> SignUpClientAsync(DCClientSignUp SignUpInfos);
        public Task<DApiResponse<object?>> UpdateClientProfileAsync(DCUpdateClientProfileInfo Form, int Id);
        public Task<DApiResponse<object?>> GetClientInfoAsync(int ClientId);
        public Task<DApiResponse<object?>> ResetPasswordAsync(DResetPassword Form, int Id);
    }
}
