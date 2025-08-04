using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.ClientDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces
{
    public interface IClientRegistrationService
    {
        public Task<ApiResponseDto<TokenResponseDto?>> SignUpClientAsync(ClientSignUpDto SignUpInfos);
        public Task<ApiResponseDto<object?>> UpdateClientProfileAsync(UpdateClientProfileInfoDto Form, int Id);
        public Task<ApiResponseDto<object?>> GetClientInfoAsync(int ClientId);
        public Task<ApiResponseDto<object?>> ResetPasswordAsync(ResetPasswordDto Form, int Id);
    }
}
