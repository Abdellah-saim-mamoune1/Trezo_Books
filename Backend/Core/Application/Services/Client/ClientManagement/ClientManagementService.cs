using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.ClientDTO_s;
using EcommerceBackend.UtilityClasses;



namespace EcommerceBackend.Core.Application.Services.ClientServices.ClientManagementServices
{
    public class ClientManagementService
        (

        IClientManagementValidatorsService _Validate
        ,IClientManagementRepository _Repo

        ) :IClientRegistrationService
    {

        public async Task<ApiResponseDto<TokenResponseDto?>> SignUpClientAsync(ClientSignUpDto SignUpInfos)
        {
            List<ValidationErorrsDto> Errors = new();
            var ValidationErrors = await _Validate.ValidateSignUp(SignUpInfos);

            if (ValidationErrors != null)
            {
                return UApiResponder<TokenResponseDto?>.Fail("Invalid Pieces of Information", ValidationErrors, 400);
            }
            var data = await _Repo.RegisterClientAsync(SignUpInfos);
            if( data ==null)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Server", Message = "Internal Server Error" });
                return UApiResponder<TokenResponseDto?>.Fail("Internal server error", Errors, 500);
            }
            return UApiResponder<TokenResponseDto>.Success(data, "Client was registered successfully");
        }

        public async Task<ApiResponseDto<object?>> GetClientInfoAsync(int ClientId)
        {
            List<ValidationErorrsDto> Errors = new();
            var ValidationErrors = await _Validate.ValidateGet(ClientId);

            if (ValidationErrors != null)
            {
                return UApiResponder<object?>.Fail("Invalid Pieces of Information.", ValidationErrors, 400);
            }
            var data = await _Repo.GetClientInfoByIdAsync(ClientId);
            if (data == null)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Server.", Message = "Internal Server Error." });
                return UApiResponder<object?>.Fail("Internal server error.", Errors, 500);
            }
            return UApiResponder<object>.Success(data, "Client info were fetched successfully.");
        }


        public async Task<ApiResponseDto<object?>> UpdateClientProfileAsync(UpdateClientProfileInfoDto Form,int Id)
        {
            List<ValidationErorrsDto> Errors = new();
            var ValidationErrors = await _Validate.ValidateUpdate(Form,Id);

            if (ValidationErrors != null)
            {
                return UApiResponder<object?>.Fail("Invalid Pieces of Information", ValidationErrors, 400);
            }
            var data = await _Repo.UpdateProfileInfoAsync(Form,Id);
            if (data == ValidationStatus.Fail)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Server", Message = "Internal Server Error" });
                return UApiResponder<object?>.Fail("Internal server error", Errors, 500);
            }
            return UApiResponder<object?>.Success(null, "Client was updated successfully");
        }


        public async Task<ApiResponseDto<object?>> ResetPasswordAsync(ResetPasswordDto Form, int Id)
        {
            List<ValidationErorrsDto> Errors = new();
            var ValidationErrors = await _Validate.ValidateResetPassword(Form, Id);

            if (ValidationErrors != null)
            {
                return UApiResponder<object?>.Fail("Invalid Pieces of Information", ValidationErrors, 400);
            }
            var data = await _Repo.ResetPasswordAsync(Form, Id);
            if (data == ValidationStatus.Fail)
            {
                Errors.Add(new ValidationErorrsDto { FieldId = "Server", Message = "Internal Server Error" });
                return UApiResponder<object?>.Fail("Internal server error", Errors, 500);
            }
            return UApiResponder<object?>.Success(null, "password was updated successfully");
        }


    }
}
