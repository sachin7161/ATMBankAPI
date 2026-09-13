using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

    }
}
