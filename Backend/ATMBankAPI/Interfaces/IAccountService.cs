using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponseDto> CreateAccount(AccountDto dto);
        Task<AccountResponseDto> GetAccountByNumber(long accountNumber);
        Task<AccountDashboardDto> GetAccountDashboard(long accountNumber);
        Task<long> GetAccountNumberByUserId(int userId);
    }
}
