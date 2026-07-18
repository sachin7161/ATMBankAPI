namespace ATMBankAPI.Dtos
{
    public class BalanceInquiryDto
    {
        public long AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Status { get; set; }
        public string BranchName { get; set; }
    }
}
