using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class CardStatusDto
    {
        [Required]
        public long AccountNumber { get; set; }

    }
}
