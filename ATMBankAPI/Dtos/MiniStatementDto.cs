namespace ATMBankAPI.Dtos
{
    public class MiniStatementDto
    {
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }

    }
}
