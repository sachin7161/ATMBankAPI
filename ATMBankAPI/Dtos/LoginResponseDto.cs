namespace ATMBankAPI.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserName { get; set; }
        public string Roel { get; set; }
    }
}
