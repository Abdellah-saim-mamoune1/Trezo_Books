using EcommerceBackend.Core.Domain.Models.ClientModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.OrderModels
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [Required]
        public int TotalQuantity { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public string ShipmentAddress { get; set; } = string.Empty;
        [Required]
        public string Status { get; set; }=string .Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ArrivedAt { get; set; }
        public IEnumerable<OrderItem>? OrderItems { get; set; }
        public Client? client { get; set; }

    }
}
