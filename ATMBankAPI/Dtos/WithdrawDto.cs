using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class WithdrawDto
    {
        [Required]
        public long AccountNumber { get; set; }


        [Required]
        [Range(1, 10000000)]
        public decimal Amount { get; set; }

        public string Description { get; set; } 


    }
}
