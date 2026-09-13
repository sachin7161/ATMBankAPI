namespace ATMBankAPI.Dtos
{
    public class BeneficiaryTransferResponseDto
    {
        public string Message { get; set; } = string.Empty;

        public long FromAccountNumber { get; set; }

        public long ToAccountNumber { get; set; }

        public decimal TransferAmount { get; set; }

        public decimal PreviousBalance { get; set; }

        public decimal CurrentBalance { get; set; }

        public string ReferenceNumber { get; set; } = string.Empty;

        public DateTime TransactionDate { get; set; }
    }
}
