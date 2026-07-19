using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class AtmCardDto
    {
        [Required]
        public long AccountNumber { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$",ErrorMessage ="Pin must $ digits")]
        public string Pin {  get; set; }
        public decimal DailyLimit { get; set; } = 25000;
    }
}
