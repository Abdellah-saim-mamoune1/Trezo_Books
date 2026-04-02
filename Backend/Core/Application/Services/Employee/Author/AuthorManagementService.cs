using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Application.Utilities;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.AuthorServices
{
    public class AuthorManagementService
        (
        IAuthorRepository _Repo,
        IAuthorManagementValidationService _Validate
        ) : IAuthorManagementService
    {
        public async Task<ApiResponseDto<object?>> CreateAuthorAsync(AuthorDto author)
        {
            var Errors = await _Validate.ValidateAdd(author);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid author data", Errors, 400);
            }

            int AuthorId = await _Repo.Create(author);
            if (AuthorId == -1)
            {
                var ServerError = new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Server", Message = "Failed to insert author into database." } };
                return UApiResponder<object>.Fail("Internal server error", ServerError, 500);
            }

            return UApiResponder<object>.Success(AuthorId, "Author was created successfully.");
        }

        public async Task<ApiResponseDto<object?>> UpdateAuthorAsync(AuthorGetXUpdateDto author)
        {
            var Errors = await _Validate.ValidateUpdate(author);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid author data", Errors, 400);
            }

            if (await _Repo.Update(author) == false)
            {
                var ServerError = new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Server", Message = "Internal server error" } };
                return UApiResponder<object>.Fail("Internal server error", ServerError, 500);
            }

            return UApiResponder<object>.Success(null, "Author was updated successfully.");
        }

        public async Task<ApiResponseDto<object?>> DeleteAuthorAsync(int AuthorId)
        {
            var Errors = await _Validate.ValidateDelete(AuthorId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid author data", Errors, 400);
            }

            if (await _Repo.Delete(AuthorId) == false)
            {
                var ServerError = new List<ValidationErorrsDto> { new ValidationErorrsDto { FieldId = "Server", Message = "Internal server error" } };
                return UApiResponder<object>.Fail("Internal server error", ServerError, 500);
            }

            return UApiResponder<object>.Success(null, "Author was deleted successfully.");
        }

        public async Task<ApiResponseDto<object?>> GetPaginatedAuthorsAsync(PaginationFormDto Form)
        {
            var Errors = _Validate.ValidateGetPaginated(Form);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pagination data", Errors, 400);
            }

            var Authors = await _Repo.GetPaginatedAuthorsAsync(Form);

            return UApiResponder<object>.Success(Authors, "Authors were fetched successfully.");
        }

        public async Task<ApiResponseDto<object?>> GetAuthorByIdAsync(int Id)
        {
            var Author = await _Repo.GetAuthorByIdAsync(Id);

            return UApiResponder<object>.Success(Author, Author == null ? "Author not found." : "Author was fetched successfully.");
        }
    }
}