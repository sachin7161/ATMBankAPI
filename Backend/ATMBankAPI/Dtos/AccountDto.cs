using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class AccountDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int BranchId { get; set; }
        [Required]
        public string AccountType { get; set; }

        [Required]
        public decimal OpningBalance { get; set; }
    }
}
