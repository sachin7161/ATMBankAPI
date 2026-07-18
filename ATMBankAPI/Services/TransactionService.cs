using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;

namespace ATMBankAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

       

        public async Task<DepositResponseDto> Deposit(DepositDto dto)
        {
            return await _repository.Deposit(dto);
        }

        public async Task<BalanceInquiryDto> GetBalance(long accountnumber)
        {
            return await _repository.GetBalance(accountnumber);
        }

        public async Task<List<MiniStatementDto>> GetMiniStatement(long accountnumber)
        {
            return await _repository.GetMiniStatement(accountnumber);
        }

        public async Task<WithdrawResponseDto> Withdraw(WithdrawDto dto)
        {
            return await _repository.Withdraw(dto);
        }
    }
}
