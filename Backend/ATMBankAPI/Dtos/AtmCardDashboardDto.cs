namespace ATMBankAPI.Dtos
{
    public class AtmCardDashboardDto
    {
        public long CardNumber { get; set; }
        public DateOnly ExpiryDate { get; set; }
        public decimal DailyLimit { get; set; }
        public string? CardStatus { get; set; }
    }
}
