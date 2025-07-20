using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s
{
    public class DCGetPaginatedBooksTypes:DPaginationForm
    {
        public string Type { get; set; } = string.Empty;
    }
}
