using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface ILoanService
    {
        Task<ApplyLoanResponseDto> ApplyLoan(ApplyLoanDto dto);
        Task<GetLoanDto> GetLoan(int loanId);
        Task<LoanStatusResponseDto> ApproveLoan(UpdateLoanStatusDto dto);
        Task<LoanStatusResponseDto> RejectLoan(UpdateLoanStatusDto dto);
    }
}
