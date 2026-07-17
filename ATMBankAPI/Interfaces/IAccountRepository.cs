using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task<AccountResponseDto> CreateAccount(CreateAccountDto dto);
    }
}
