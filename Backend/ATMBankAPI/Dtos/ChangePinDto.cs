using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class ChangePinDto
    {
        [Required]
        public long AccountNumber { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$",ErrorMessage ="Pin Must be 4 digit")]
        public string OldPin { get; set; }
        [Required]
        [RegularExpression(@"^\d{4}$",ErrorMessage ="Pin  Must be 4 digit")]
        public string NewPin { get; set; }  
    }
}
