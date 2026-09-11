using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountResponseDto> CreateAccount(AccountDto dto);

        Task<AccountResponseDto> GetAccountByNumber(long accountNumber);

        Task<bool> IsAccountOwnedByUser(long accountNumber, int userId);
        Task<AccountDashboardDto> GetAccountDashboard(long accountNumber);
    }
}