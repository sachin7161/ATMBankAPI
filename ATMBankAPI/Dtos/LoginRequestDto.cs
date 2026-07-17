using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
