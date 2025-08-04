using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.CBookTypeDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;

namespace EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CBookCopyServicesInterfaces
{
    public interface IBooksCopiesGetService
    {
        public Task<ApiResponseDto<object?>> GetInitialBooksCopiesDataPerType();
        public Task<ApiResponseDto<object?>> GetInitialBooksCopiesDataByType(GetPaginatedBooksTypesDto form);
        public Task<ApiResponseDto<object?>> GetInitialNewReleasedBooksCopiesData();
        public Task<ApiResponseDto<object?>> GetInitialBestSellersBooksCopiesData();
        public Task<ApiResponseDto<object?>> GetInitialRecommendedBooksCopiesData();
        public Task<ApiResponseDto<object?>> GetInitialTopRatedBooksCopiesData();
        public Task<ApiResponseDto<object?>> GetBooksTypes();
        public Task<ApiResponseDto<object?>> GetBooksCopiesInfoByName(string Name);
        public Task<ApiResponseDto<object?>> GetBookCopyInfoById(int Id);
    }
}
