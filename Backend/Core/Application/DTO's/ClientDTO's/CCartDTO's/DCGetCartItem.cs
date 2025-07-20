namespace EcommerceBackend.DTO_s.CartDTO_s
{
    public class DCGetCartItem
    {
        public int Id { get; set; }
        public int  ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalQuantity { get; set; }
        public int BookCopyQuantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

    }
}
