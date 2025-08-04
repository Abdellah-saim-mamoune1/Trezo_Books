namespace EcommerceBackend.Core.Application.DTO_s.SharedDTO_s
{
    public class ResetPasswordDto
    {
        public string OldPassword { get; set; }=string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
