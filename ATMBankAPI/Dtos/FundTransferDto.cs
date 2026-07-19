using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class FundTransferDto
    {
        [Required]
        public long FromAccountNumber { get; set; }

        [Required]
        public long ToAccountNumber { get; set; }

        [Required]
        [Range(1, 10000000)]
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
