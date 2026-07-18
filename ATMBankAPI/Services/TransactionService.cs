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
    }
}
