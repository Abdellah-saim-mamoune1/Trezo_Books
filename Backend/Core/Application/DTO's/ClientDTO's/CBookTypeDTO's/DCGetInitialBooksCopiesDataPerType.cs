namespace EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s
{
    public class DCGetInitialBooksCopiesDataPerType
    {
        public string Type { get; set; }= string.Empty;
        public IEnumerable<DCGetInitialBookCopyData>? BooksCopies { get; set; }
    }
}
