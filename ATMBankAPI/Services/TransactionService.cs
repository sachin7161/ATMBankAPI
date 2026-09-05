using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Repository;

namespace ATMBankAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionService(
            ITransactionRepository repository,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<DepositResponseDto> Deposit(DepositDto dto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _repository.IsAccountOwnedByUser(
                dto.AccountNumber,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this account.");
            }

            return await _repository.Deposit(dto);
        }

        public async Task<List<TransactionResponseDto>> FilterTransactions(TransactionFilterDto dto)
        {
            return await _repository.FilterTransactions(dto);
        }

        public async Task<FundTransferResponseDto> FundTransfer(FundTransferDto dto)
        {
           return await _repository.FundTransfer(dto);
        }

        public async Task<List<TransactionResponseDto>> GetAllTransactions(int accountId)
        {
            return await _repository.GetAllTransactions(accountId);
        }

        public async Task<BalanceInquiryDto> GetBalance(long accountnumber)
        {
            return await _repository.GetBalance(accountnumber);
        }

        public async Task<List<MiniStatementDto>> GetMiniStatement(long accountnumber)
        {
            return await _repository.GetMiniStatement(accountnumber);
        }

        public async Task<TransactionResponseDto> GetTransaction(int transactionId)
        {
            return await _repository.GetTransaction(transactionId);
        }

        public async Task<WithdrawResponseDto> Withdraw(WithdrawDto dto)
        {
            return await _repository.Withdraw(dto);
        }
    }
}
