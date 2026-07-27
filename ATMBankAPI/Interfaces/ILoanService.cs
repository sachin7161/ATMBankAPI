using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface ILoanService
    {
        Task<ApplyLoanResponseDto> ApplyLoan(ApplyLoanDto dto);
    }
}
