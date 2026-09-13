using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class UpdateLoanStatusDto
    {
        [Required]
        public int LoanId { get; set; }

    }
}
