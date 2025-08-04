using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces
{
    public interface IAuthenticationValidationService
    {
        public Task<List<ValidationErorrsDto>?> ValidateLogin(LoginDto request);
        public Task<List<ValidationErorrsDto>?> ValidateRefreshToken(RefreshTokenRequestDto request);
    }
}
