namespace ATMBankAPI.Dtos
{
    public class AtmCardResponseDto
    {
        public string Message { get; set; }
        public long CardNumber { get; set; }
        public string CVV { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public decimal DailyLimit { get; set; }
        public string CardStatus { get; set; }
    }
}
