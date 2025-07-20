using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.AuthenticationRepositories
{
    public interface ITokensRepository
    {
        public Task<DTokenResponse?> GenerateAndSaveTokens(DGenerateTokensInfos GTI);
        public Task<DTokenResponse?> RefreshTokensAsync(DRefreshTokenRequest request);
    }
}
