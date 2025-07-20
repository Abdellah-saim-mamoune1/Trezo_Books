using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.SharedDTO_s;
using EcommerceBackend.UtilityClasses;


namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EAuthorServices
{
    public class EAuthorManagementService(

        IEAuthorManagementValidationService _Validate,
        IEAuthorRepository _Repository
        ): IEAuthorManagementService
    {
        public async Task<DApiResponse<object?>> CreateAuthorAsync(DEAuthor author)
        {
            var Errors = await _Validate.ValidateAdd(author);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }
            int AuthorId= await _Repository.CreateAsync(author);
            if (AuthorId == -1) {
                var ServerError = new List<DValidationErorrs> { new DValidationErorrs { FieldId = "Server.", Message = "Internal server Error." } };
                return UApiResponder<object>.Fail("Internal server error.", ServerError, 500);
            }
            
            return UApiResponder<object>.Success(AuthorId, "Author was created successfully.");
        }


        public async Task<DApiResponse<object?>> UpdateAuthorAsync(DEAuthorGetXUpdate author)
        {
            var Errors = await _Validate.ValidateUpdate(author);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }
           
            if (await _Repository.UpdateAsync(author) == ValidationStatus.Fail)
            {
                var ServerError = new List<DValidationErorrs> { new DValidationErorrs { FieldId = "Server.", Message = "Internal server Error." } };
                return UApiResponder<object>.Fail("Internal server error.", ServerError, 500);
            }

            return UApiResponder<object>.Success(null, "Author was updated successfully.");
        }

        public async Task<DApiResponse<object?>> DeleteAuthorAsync(int AuthorId)
        {
            var Errors = await _Validate.ValidateDelete(AuthorId);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }
  
            if (await _Repository.DeleteAsync(AuthorId)== ValidationStatus.Fail)
            {
                var ServerError = new List<DValidationErorrs> { new DValidationErorrs { FieldId = "Server.", Message = "Internal server Error." } };
                return UApiResponder<object>.Fail("Internal server error.", ServerError, 500);
            }

            return UApiResponder<object>.Success(null, "Author was deleted successfully.");
        }

        public async Task<DApiResponse<object?>> GetPaginatedAuthorAsync(DPaginationForm Form)
        {
            var Errors = await _Validate.ValidateGetPaginated(Form);

            if (Errors != null)
            {
                return UApiResponder<object>.Fail("Invalid pieces of information.", Errors, 400);
            }

            var Authors = await _Repository.GetPaginatedAuthorsAsync(Form);

            return UApiResponder<object>.Success(Authors, "Authors were fetched successfully.");
        }

        public async Task<DApiResponse<object?>> GetAuthorByIdAsync(int Id)
        {

            var Author = await _Repository.GetAuthorByIdAsync(Id);
            return UApiResponder<object>.Success(Author,Author==null? "Author not found.": "Author was fetched successfully.");
        }

        public async Task<DApiResponse<object?>> GetAuthorByNameAsync(string Name)
        {

            var Author = await _Repository.GetAuthorByName(Name);
            return UApiResponder<object>.Success(Author, Author == null ? "Author not found." : "Author was fetched successfully.");
        }


    }
}
