using EcommerceBackend.DTO_s.CartDTO_s;
namespace EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s
{
    public class AddOrderDto
    {
        public string ShipmentAddress { get; set; } = string.Empty;
        public int TotalQuantity { get; set; }
        public float TotalPrice { get; set; }
        public List<int>? CartItemsIds { get; set; }
    }
}
