using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.AuthenticationRepositories
{
    public interface ITokensRepository
    {
        public Task<TokenResponseDto?> GenerateAndSaveTokens(GenerateTokensInfosDto GTI);
        public Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
    }
}
