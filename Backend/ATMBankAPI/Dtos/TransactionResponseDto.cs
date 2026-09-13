namespace ATMBankAPI.Dtos
{
    public class TransactionResponseDto
    {
        public int TransactionId { get; set; }

        public long AccountNumber { get; set; }

        public string TransactionType { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ReferenceNumber { get; set; } = string.Empty;

        public DateTime TransactionDate { get; set; }
    }
}
