using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class ApplyLoanDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int LoanTypeId { get; set; }
        [Required]
        [Range(1000,double.MaxValue)]
        public decimal LoanAmount { get; set; }
        [Required]
        public int DurationMonths { get; set; }
    }
}
