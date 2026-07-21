namespace ATMBankAPI.Dtos
{
    public class GetAtmCardDto
    {
        public long CardNumber { get; set; }
        public long AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public DateOnly ExpirayDate { get; set; }
        public decimal DailyLimit { get; set; }
        public string CardStatus { get; set; }

    }
}
