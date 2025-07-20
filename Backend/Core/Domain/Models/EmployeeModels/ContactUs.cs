namespace EcommerceBackend.Core.Domain.Models.EmployeeModels
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string UserName { get; set; }=string.Empty;
        public string Account { get; set; } = string.Empty;
        public string Message { get; set; }= string.Empty;
        public DateOnly? CreatedAt { get; set; }

    }
}
