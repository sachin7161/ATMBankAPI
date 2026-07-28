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

        public async Task<LoanStatusResponseDto> ApproveLoan(UpdateLoanStatusDto dto)
        {
            var loan = await _contex.Loans
         .FirstOrDefaultAsync(l => l.LoanId == dto.LoanId);

            if (loan == null)
            {
                throw new Exception("Loan not found.");
            }

            if (loan.LoanStatus == "Approved")
            {
                throw new Exception("Loan is already approved.");
            }

            if (loan.LoanStatus == "Rejected")
            {
                throw new Exception("Rejected loan cannot be approved.");
            }

            loan.LoanStatus = "Approved";

            await _contex.SaveChangesAsync();

            return new LoanStatusResponseDto
            {
                Message = "Loan approved successfully.",
                LoainId = loan.LoanId,
                LoanStatus = loan.LoanStatus!
            };
        }

        public async Task<GetLoanDto> GetLoan(int loanId)
        {
            var loan = await _contex.Loans.Include(e => e.Customer).Include(e => e.LoanTypeNavigation).FirstOrDefaultAsync(e => e.LoanId == loanId);
            if(loan == null)
            {
                throw new Exception("Loan not Found");
            }

            GetLoanDto londto = new GetLoanDto
            {
                LoanId = loanId,
                CustomerName = loan.Customer.FirstName + " " + loan.Customer.LastName,
                LoanType = loan.LoanTypeNavigation.LoneTypeName,
                LoanAmount = loan.LoanAmount ?? 0,
                Emi = loan.Emi ?? 0,
                DurationMonth = loan.DurationMonths ?? 0,
                LoanStatus = loan.LoanStatus,
                ApplyDate = loan.ApplyDate ?? DateTime.MinValue,

            };
            return londto;
        }

        public async Task<LoanStatusResponseDto> RejectLoan(UpdateLoanStatusDto dto)
        {
            var loan=await _contex.Loans.FirstOrDefaultAsync(e=>e.LoanId == dto.LoanId);
            if(loan == null)
            {
                throw new Exception("Loan Not Found");

            }
            if(loan.LoanStatus== "Rejected")
            {
                throw new Exception("Loan Is Alerady Rejected");
            }

            loan.LoanStatus = "Rejected";
            await _contex.SaveChangesAsync();

            LoanStatusResponseDto resdto = new LoanStatusResponseDto
            {
                Message = "Loan Rejected Successfull",
                LoainId = loan.LoanId,
                LoanStatus = loan.LoanStatus,
            };
            return resdto;

        }
    }
}
