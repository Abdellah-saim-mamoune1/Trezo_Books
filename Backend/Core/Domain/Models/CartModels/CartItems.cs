using EcommerceBackend.Core.Domain.Models.BookModels;
using EcommerceBackend.Core.Domain.Models.ClientModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBackend.Core.Domain.Models.CartModels
{
    public class CartItems
    {
        public int Id { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [ForeignKey("BookCopy")]
        public int BookCopyId { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; } 
        public Client? client { get; set; }
        public BookCopy? bookCopy { get; set; }
    }
}
