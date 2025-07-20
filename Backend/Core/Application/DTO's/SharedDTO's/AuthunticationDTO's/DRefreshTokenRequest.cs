namespace EcommerceBackend.DTO_s.AuthunticationDTO_S
{
    public class DRefreshTokenRequest
    {
        public required string RefreshToken { get; set; }
        public required string Role { get; set; }
    }
}
