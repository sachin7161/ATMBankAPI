using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class WithdrawDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "Account number must be greater than 0.")]
        public long AccountNumber { get; set; }

        [Range(0.01, 10000000, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        public string? Description { get; set; }
    }
}