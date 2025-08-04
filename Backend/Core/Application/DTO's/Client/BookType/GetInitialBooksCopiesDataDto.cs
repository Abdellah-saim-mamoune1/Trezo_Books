namespace EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s
{
    public class DCGetInitialBookCopyData
    {

        public required int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public float Price { get; set; }
        public string AuthorName { get; set; }= string.Empty;
    }
}
