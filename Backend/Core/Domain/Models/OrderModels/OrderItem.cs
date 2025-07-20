
using EcommerceBackend.Core.Domain.Models.BookModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.OrderModels
{
    public class OrderItem
    {
        public int Id { get; set; }
        [ForeignKey("BookCopy")]
        public int BookCopyId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public Order? order { get; set; }
        public BookCopy? BookCopy { get; set; }
    }
}
