using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
namespace ATMBankAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<ApplyLoanResponseDto> ApplyLoan(ApplyLoanDto dto)
        {
            return await _loanRepository.ApplyLoan(dto);
        }
    }
}
