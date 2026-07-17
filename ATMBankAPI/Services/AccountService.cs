using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;

namespace ATMBankAPI.Services
{
    public class AccountService : IAccountService
    {
        public readonly IAccountRepository _accountreposittory;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountreposittory = accountRepository;
        }
        public async Task<AccountResponseDto> CreateAccount(CreateAccountDto dto)
        {
            return await _accountreposittory.CreateAccount(dto);
        }
    }
}
