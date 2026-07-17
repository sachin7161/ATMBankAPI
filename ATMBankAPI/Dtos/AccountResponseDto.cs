namespace ATMBankAPI.Dtos
{
    public class AccountResponseDto
    {
        public int AccountId { get; set; }
        public long AccountNumber { get; set; }

        public string CustomerName { get; set; }

        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }
        public DateTime? OpenDate { get; set; }
    }
}
