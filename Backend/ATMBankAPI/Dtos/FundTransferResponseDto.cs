namespace ATMBankAPI.Dtos
{
    public class FundTransferResponseDto
    {
        public string Message { get; set; }
        public string ReferanceNumber { get; set; }
        public long FromAccountNumber { get; set; }
        public long ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal RemainingBalance { get; set; }
    }
}

