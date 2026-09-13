namespace ATMBankAPI.Dtos
{
    public class CustomerDashboardDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
