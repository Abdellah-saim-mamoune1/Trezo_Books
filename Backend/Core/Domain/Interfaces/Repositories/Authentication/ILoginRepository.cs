using EcommerceBackend.DTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.AuthenticationRepositories
{
    public interface ILoginRepository
    {
        public Task<TokenResponseDto?> LoginAsync(LoginDto request);
    }
}
