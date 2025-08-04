using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using static EcommerceBackend.UtilityClasses.ValidationStatus;
namespace EcommerceBackend.UtilityClasses
{
    public class UApiResponder<T>
    {
        public static ApiResponseDto<T?> Success(T? data , string message = "Success", int status = 200)
        => new() { Status = status, Success = true, Title = message, Data = data ,Errors=default};

        public static ApiResponseDto<T?> Fail(string message,IEnumerable<ValidationErorrsDto>? Errors, int status = 400)
            => new() { Status = status, Success = false, Title = message,Errors=Errors, Data = default };



        public static ApiResponseDto<T?> GetFailResponseObject(IEnumerable<ValidationErorrsDto>? Errors, ErrorTypes Type)
        {
            string Message = Type == ErrorTypes.BadRequest ? "Invalid pieces of information" : "Server Error";
            int Status = Type == ErrorTypes.BadRequest ? 400 : 500;
            return new ApiResponseDto<T?>
            {
                Success = false,
                Title = Message,
                Status = Status,
                Errors = Errors
            };

        }

    }
}
