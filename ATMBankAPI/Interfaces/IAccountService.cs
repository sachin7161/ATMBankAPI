using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponseDto> CreateAccount(CreateAccountDto dto);
    }
}
