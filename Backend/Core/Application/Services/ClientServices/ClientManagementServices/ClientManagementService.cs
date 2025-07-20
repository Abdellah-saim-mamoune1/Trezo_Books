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

        ICClientManagementValidatorsService _Validate
        ,ICClientManagementRepository _Repo

        ) :ICClientRegistrationService
    {

        public async Task<DApiResponse<DTokenResponse?>> SignUpClientAsync(DCClientSignUp SignUpInfos)
        {
            List<DValidationErorrs> Errors = new();
            var ValidationErrors = await _Validate.ValidateSignUp(SignUpInfos);

            if (ValidationErrors != null)
            {
                return UApiResponder<DTokenResponse?>.Fail("Invalid Pieces of Information", ValidationErrors, 400);
            }
            var data = await _Repo.RegisterClientAsync(SignUpInfos);
            if( data ==null)
            {
                Errors.Add(new DValidationErorrs { FieldId = "Server", Message = "Internal Server Error" });
                return UApiResponder<DTokenResponse?>.Fail("Internal server error", Errors, 500);
            }
            return UApiResponder<DTokenResponse>.Success(data, "Client was registered successfully");
        }

        public async Task<DApiResponse<object?>> GetClientInfoAsync(int ClientId)
        {
            List<DValidationErorrs> Errors = new();
            var ValidationErrors = await _Validate.ValidateGet(ClientId);

            if (ValidationErrors != null)
            {
                return UApiResponder<object?>.Fail("Invalid Pieces of Information.", ValidationErrors, 400);
            }
            var data = await _Repo.GetClientInfoByIdAsync(ClientId);
            if (data == null)
            {
                Errors.Add(new DValidationErorrs { FieldId = "Server.", Message = "Internal Server Error." });
                return UApiResponder<object?>.Fail("Internal server error.", Errors, 500);
            }
            return UApiResponder<object>.Success(data, "Client info were fetched successfully.");
        }


        public async Task<DApiResponse<object?>> UpdateClientProfileAsync(DCUpdateClientProfileInfo Form,int Id)
        {
            List<DValidationErorrs> Errors = new();
            var ValidationErrors = await _Validate.ValidateUpdate(Form,Id);

            if (ValidationErrors != null)
            {
                return UApiResponder<object?>.Fail("Invalid Pieces of Information", ValidationErrors, 400);
            }
            var data = await _Repo.UpdateProfileInfoAsync(Form,Id);
            if (data == ValidationStatus.Fail)
            {
                Errors.Add(new DValidationErorrs { FieldId = "Server", Message = "Internal Server Error" });
                return UApiResponder<object?>.Fail("Internal server error", Errors, 500);
            }
            return UApiResponder<object?>.Success(null, "Client was updated successfully");
        }


        public async Task<DApiResponse<object?>> ResetPasswordAsync(DResetPassword Form, int Id)
        {
            List<DValidationErorrs> Errors = new();
            var ValidationErrors = await _Validate.ValidateResetPassword(Form, Id);

            if (ValidationErrors != null)
            {
                return UApiResponder<object?>.Fail("Invalid Pieces of Information", ValidationErrors, 400);
            }
            var data = await _Repo.ResetPasswordAsync(Form, Id);
            if (data == ValidationStatus.Fail)
            {
                Errors.Add(new DValidationErorrs { FieldId = "Server", Message = "Internal Server Error" });
                return UApiResponder<object?>.Fail("Internal server error", Errors, 500);
            }
            return UApiResponder<object?>.Success(null, "password was updated successfully");
        }


    }
}
