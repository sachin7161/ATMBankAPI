using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface ITransactionRepository
    {
        Task<DepositResponseDto> Deposit(DepositDto dto);
        Task<WithdrawResponseDto> Withdraw(WithdrawDto dto);
        Task<BalanceInquiryDto> GetBalance(long accountnumber);
    }
}
