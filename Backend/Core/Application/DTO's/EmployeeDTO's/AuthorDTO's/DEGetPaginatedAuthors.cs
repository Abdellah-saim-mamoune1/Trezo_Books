using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s
{
    public class DEGetPaginatedAuthors: DPagination
    {
       public IEnumerable<DEAuthorGetXUpdate>? Authors { get; set; }
    }
}
