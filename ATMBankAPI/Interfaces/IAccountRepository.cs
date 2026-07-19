using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountResponseDto> CreateAccount(AccountDto dto);
    }
}
