namespace EcommerceBackend.DTO_s.AuthunticationDTO_S
{
    public class TokenResponseDto
    {
        public string Role { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
