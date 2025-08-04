using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s
{
    public class GetPaginatedAuthorsDto: PaginationDto
    {
       public IEnumerable<AuthorGetXUpdateDto>? Authors { get; set; }
    }
}
