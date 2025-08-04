using EcommerceBackend.Core.Domain.Models.ClientXEmployeeModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.ClientModels
{
    public class ClientAccount
    {
        public int Id { get; set; }
        public string Account { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [ForeignKey("Token")]
        public int TokenId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Client? Client { get; set; }
        public Token? Token { get; set; }
    }
}
