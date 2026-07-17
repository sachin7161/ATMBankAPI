using System.ComponentModel.DataAnnotations;

namespace ATMBankAPI.Dtos
{
    public class CreateCustomerDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Gender { get; set; }
        public DateOnly Dob { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? AadharNo { get; set; }
        public string? PanNo { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string Pincode { get; set; }
    }
}
