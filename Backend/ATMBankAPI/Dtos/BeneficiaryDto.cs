using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class BeneficiaryDto
    {
        public int AccountId { get; set; }
        [Required]
        public string BeneficiaryName { get; set; }

        [Required]
        public long BeneficiaryAccount { get; set; }

        [Required]
        public string Ifsccode { get; set;  }
        public string? NickName { get; set; } = null;
    }
}
