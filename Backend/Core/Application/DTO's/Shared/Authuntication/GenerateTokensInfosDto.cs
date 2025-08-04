namespace EcommerceBackend.DTO_s.AuthunticationDTO_S
{
    public class GenerateTokensInfosDto
    {
        public required string Id { get; set; }
        public required string Role { get; set; } = string.Empty;
        public required int TokenId { get; set; }

    }
}
