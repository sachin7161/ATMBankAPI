namespace ATMBankAPI.Dtos
{
    public class AccountDashboardDto
    {
        public AccountResponseDto Account { get; set; }

        public CustomerDashboardDto Customer { get; set; }

        public AtmCardDashboardDto? AtmCard { get; set; }

        public decimal Balance { get; set; }

        public List<TransactionResponseDto> RecentTransactions { get; set; }

        public List<LoanHistoryDto> Loans { get; set; }
    }
}
