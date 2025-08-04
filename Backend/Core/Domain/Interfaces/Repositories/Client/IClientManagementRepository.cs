using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.ClientManagementDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.ClientDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface IClientManagementRepository
    {
        public Task<TokenResponseDto?> RegisterClientAsync(ClientSignUpDto SignUpInfos);
        public Task<GetClientInfoDto?> GetClientInfoByIdAsync(int ClientId);
        public Task<bool> ResetPasswordAsync(ResetPasswordDto data, int ClientId);
        public Task<bool> UpdateProfileInfoAsync(UpdateClientProfileInfoDto Form, int Id);
    }
}
