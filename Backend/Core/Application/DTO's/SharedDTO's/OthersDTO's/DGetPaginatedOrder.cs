using EcommerceBackend.DTO_s.SharedDTO_s;

namespace EcommerceBackend.Core.Application.DTO_s.ClientDTO_s.COrderDTO_s
{
    public class DGetPaginatedOrder: DPagination
    {
      public IEnumerable<CDGetOrder>? Orders { get; set; }
    }

    public class CDGetOrder
    {
        public int Id { get; set; }
        public required int ClientId { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public string ShipmentAddress { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ArrivedAt { get; set; }
        public IEnumerable<CDGetOrderItem>? Items { get; set; }

    }

    public class CDGetOrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
