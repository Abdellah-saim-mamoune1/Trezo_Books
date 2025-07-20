namespace EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.StatisticsDTO_s
{
    public class DEGetResentOrders
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }=string.Empty;
    }
}
