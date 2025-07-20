using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;

namespace EcommerceBackend.DTO_s.AuthunticationDTO_S
{
    public class DApiResponse<T>
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public string? Title { get; set; }
        public T? Data { get; set; }
        public IEnumerable<DValidationErorrs>? Errors { get; set; }

      
    }
}
