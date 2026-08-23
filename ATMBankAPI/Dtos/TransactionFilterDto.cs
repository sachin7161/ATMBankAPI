namespace ATMBankAPI.Dtos
{
    public class TransactionFilterDto
    {
        public int AccountId { get; set; }

        public string? TransactionType { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
