namespace ATMBankAPI.Dtos
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string? CustomerCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
