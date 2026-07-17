using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        public int? CustomerId { get; set; }
    }
}
