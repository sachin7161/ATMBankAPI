using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface ILoanRepository
    {
        Task<ApplyLoanResponseDto> ApplyLoan(ApplyLoanDto dto);
        Task<GetLoanDto> GetLoan(int loanId);
    }
}
