using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class BeneficiaryTransferDto
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public int BeneficiaryId { get; set; }

        
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Amount { get; set; }

        public string? Description { get; set; }

    }
}
