namespace EcommerceBackend.Core.Application.DTO_s.ClientDTO_s
{
    public class GetBookCopyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public float AverageRating { get; set; }
        public int RatingsCount { get; set; }
       

    }
}
