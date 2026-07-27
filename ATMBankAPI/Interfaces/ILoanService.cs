using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface ILoanService
    {
        Task<ApplyLoanResponseDto> ApplyLoan(ApplyLoanDto dto);
        Task<GetLoanDto> GetLoan(int loanId);
    }
}
