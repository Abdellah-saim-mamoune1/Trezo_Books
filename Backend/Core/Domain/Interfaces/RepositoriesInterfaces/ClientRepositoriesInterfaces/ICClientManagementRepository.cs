using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.ClientManagementDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.ClientDTO_s;

namespace EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces
{
    public interface ICClientManagementRepository
    {
        public Task<DTokenResponse?> RegisterClientAsync(DCClientSignUp SignUpInfos);
        public Task<DCGetClientInfo?> GetClientInfoByIdAsync(int ClientId);
        public Task<bool> ResetPasswordAsync(DResetPassword data, int ClientId);
        public Task<bool> UpdateProfileInfoAsync(DCUpdateClientProfileInfo Form, int Id);
    }
}
