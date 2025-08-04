namespace EcommerceBackend.DTO_s.AuthunticationDTO_S
{
    public class RefreshTokenRequestDto
    {
        public required string RefreshToken { get; set; }
        public required string Role { get; set; }
    }
}
