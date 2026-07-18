namespace ATMBankAPI.Dtos
{
    public class WithdrawResponseDto
    {
        public string Message { get; set; }
        public long AccountNumber { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal WithdrawAmount { get; set; }
        public decimal CurrentBalance { get; set; }

        public DateTime TransactionDate { get; set; }

    }
}
