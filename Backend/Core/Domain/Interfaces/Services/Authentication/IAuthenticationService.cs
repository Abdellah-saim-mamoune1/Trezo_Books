using EcommerceBackend.DTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces
{
    public interface IAuthService
    {

        public Task<ApiResponseDto<TokenResponseDto?>> Login(LoginDto request);
        public Task<ApiResponseDto<TokenResponseDto?>> RefreshTokens(RefreshTokenRequestDto request);

    }
}
