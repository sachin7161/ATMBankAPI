namespace ATMBankAPI.Dtos
{
    public class DepositResponseDto
    {
        public string Message { get; set; }
        public long AccountNUmber { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal DepositBalance { get; set; }
        public decimal CurrentBalcne { get; set; }
        public DateTime TransactionDate { get; set; }


    }
}
