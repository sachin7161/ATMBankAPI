using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ATMBankDbContext _contex;
        public LoanRepository( ATMBankDbContext contex)
        {
            _contex = contex;   
        }
        public async Task<ApplyLoanResponseDto> ApplyLoan(ApplyLoanDto dto)
        {
            var customer=await _contex.Customers.FirstOrDefaultAsync(e=>e.CustomerId==dto.CustomerId);

            if (customer == null)
            {
                throw new Exception("Customer Not Found");
            }
            var loanType = await _contex.LoanTypes.FirstOrDefaultAsync(e => e.LoneTypeId == dto.LoanTypeId);
            if(loanType == null)
            {
                throw new Exception("LoanTypeId not Found");
            }
            decimal emi = dto.LoanAmount / dto.DurationMonths;

            Loan loan = new Loan
            {
                CustomerId = dto.CustomerId,
                LoanType = dto.LoanTypeId,
                LoanAmount = dto.LoanAmount,
                Emi = emi,
                DurationMonths = dto.DurationMonths,
                LoanStatus = "Pending",
                ApplyDate = DateTime.Now,

            };
            _contex.Loans.Add(loan);
            await _contex.SaveChangesAsync();

            ApplyLoanResponseDto dto2 = new ApplyLoanResponseDto
            {
                Message = "Loan Application Submit",
                LoanId = loan.LoanId,
                CustomerName = customer.FirstName + " " + customer.LastName,
                LoanType = loanType.LoneTypeName,
                LoanAmount = loan.LoanAmount ?? 0,
                DurationMonths = loan.DurationMonths ?? 0,
                LoanStatus = loan.LoanStatus
            };
            return dto2;
        }
    }
}
