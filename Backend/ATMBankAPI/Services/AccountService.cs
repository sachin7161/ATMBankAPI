using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Repository;

namespace ATMBankAPI.Services
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository _accountreposittory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            _accountreposittory = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<AccountResponseDto> CreateAccount(AccountDto dto)
        {
            return await _accountreposittory.CreateAccount(dto);
        }

        public async Task<AccountResponseDto> GetAccountByNumber(long accountNumber)
        {
            if (accountNumber <= 0)
            {
                throw new Exception("Invalid Account Number");
            }

            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _accountreposittory.IsAccountOwnedByUser(
                accountNumber,
                userId);

            if (!isOwned)
            {
                throw new Exception(
                    "You are not authorized to access this account.");
            }

            return await _accountreposittory.GetAccountByNumber(accountNumber);
        }


        public async Task<AccountDashboardDto> GetAccountDashboard(long accountNumber)
        {
            if (accountNumber <= 0)
            {
                throw new Exception("Invalid Account Number");
            }

            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _accountreposittory.IsAccountOwnedByUser(
                accountNumber,
                userId);

            if (!isOwned)
            {
                throw new Exception(
                    "You are not authorized to access this account.");
            }

            return await _accountreposittory.GetAccountDashboard(accountNumber);
        }

        public async Task<long> GetAccountNumberByUserId(int userId)
        {
            return await _accountreposittory.GetAccountNumberByUserId(userId);
        }
    }
}
