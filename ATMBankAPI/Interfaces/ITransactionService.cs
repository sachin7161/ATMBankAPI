using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface ITransactionService
    {
        Task<DepositResponseDto> Deposit(DepositDto dto);
    }
}
