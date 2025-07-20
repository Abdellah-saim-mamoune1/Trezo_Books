using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces
{
    public interface IAuthenticationValidationService
    {
        public Task<List<DValidationErorrs>?> ValidateLogin(DLogin request);
        public Task<List<DValidationErorrs>?> ValidateRefreshToken(DRefreshTokenRequest request);
    }
}
