using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Application.DTO_s.BookDTO_s
{
    public class DEGetPaginatedBooks:DPagination
    {
        public IEnumerable<DEBookGetXUpdate>? Books { get; set; }
    }
}
