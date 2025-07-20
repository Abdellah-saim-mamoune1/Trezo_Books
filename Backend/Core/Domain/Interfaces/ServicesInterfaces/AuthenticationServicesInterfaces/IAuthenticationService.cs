using EcommerceBackend.DTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces
{
    public interface IAuthService
    {

        public Task<DApiResponse<DTokenResponse?>> Login(DLogin request);
        public Task<DApiResponse<DTokenResponse?>> RefreshTokens(DRefreshTokenRequest request);

    }
}
