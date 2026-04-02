
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.OrderModels
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Denormalized BookCopy properties - snapshot at time of order
        [Required]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; } = string.Empty;
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public float Rating { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public Order? order { get; set; }
    }
}
