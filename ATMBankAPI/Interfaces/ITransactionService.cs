using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface ITransactionService
    {
        Task<DepositResponseDto> Deposit(DepositDto dto);
        Task<WithdrawResponseDto> Withdraw(WithdrawDto dto);
        Task<BalanceInquiryDto> GetBalance(long accountnumber);
        Task<List<MiniStatementDto>> GetMiniStatement(long accountnumber);
    }
}
