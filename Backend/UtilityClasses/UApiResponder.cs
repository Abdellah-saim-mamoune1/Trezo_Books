using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using static EcommerceBackend.UtilityClasses.ValidationStatus;
namespace EcommerceBackend.UtilityClasses
{
    public class UApiResponder<T>
    {
        public static DApiResponse<T?> Success(T? data , string message = "Success", int status = 200)
        => new() { Status = status, Success = true, Title = message, Data = data ,Errors=default};

        public static DApiResponse<T?> Fail(string message,IEnumerable<DValidationErorrs>? Errors, int status = 400)
            => new() { Status = status, Success = false, Title = message,Errors=Errors, Data = default };



        public static DApiResponse<T?> GetFailResponseObject(IEnumerable<DValidationErorrs>? Errors, ErrorTypes Type)
        {
            string Message = Type == ErrorTypes.BadRequest ? "Invalid pieces of information" : "Server Error";
            int Status = Type == ErrorTypes.BadRequest ? 400 : 500;
            return new DApiResponse<T?>
            {
                Success = false,
                Title = Message,
                Status = Status,
                Errors = Errors
            };

        }

    }
}
