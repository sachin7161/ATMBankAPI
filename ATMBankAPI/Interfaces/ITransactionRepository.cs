using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface ITransactionRepository
    {
        Task<DepositResponseDto> Deposit(DepositDto dto);
    }
}
