using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class FundTransferDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "Sender account number must be greater than 0.")]
        public long FromAccountNumber { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "Receiver account number must be greater than 0.")]
        public long ToAccountNumber { get; set; }

        [Range(0.01, 10000000, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        public string? Description { get; set; }
    }
}