using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface ILoanRepository
    {
        Task<ApplyLoanResponseDto> ApplyLoan(ApplyLoanDto dto);
        Task<GetLoanDto> GetLoan(int loanId);
        Task<LoanStatusResponseDto> ApproveLoan(UpdateLoanStatusDto dto);
        Task<LoanStatusResponseDto> RejectLoan(UpdateLoanStatusDto dto);
        Task<List<LoanHistoryDto>> GetLoanHistory(int customerid);

        Task<bool> IsCustomerOwnedByUser(int customerId, int userId);
        Task<bool> IsLoanOwnedByUser(int loanId, int userId);
    }
}
