using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace ATMBankAPI.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ATMBankDbContext _context;
        public TransactionRepository(ATMBankDbContext context)
        {
            _context = context;
        }

        public async Task<DepositResponseDto> Deposit(DepositDto dto)
        {
            var account=await _context.Accounts.FirstOrDefaultAsync(e=>e.AccountNumber == dto.AccountNumber);

            if(account == null)
            {
                throw new Exception("Account Not Found");
            }

            decimal PreviousBalance = account.Balance ?? 0;
            account.Balance = PreviousBalance+dto.Amount;

            Transaction transaction = new Transaction
            {
                AccountId=account.AccountId,
                TransactionType="Deposit",
                Amount=dto.Amount,
                Description=dto.Description,
                ReferenceNumber=Guid.NewGuid().ToString(),
                TransactionDate=DateTime.Now
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return new DepositResponseDto
            {
                Message = "Amount Deposit Successfully",
                AccountNUmber = account.AccountNumber,
                PreviousBalance = PreviousBalance,
                DepositBalance = dto.Amount,
                CurrentBalcne = account.Balance ?? 0,
                TransactionDate = transaction.TransactionDate ?? DateTime.Now
            };
        }

        public async Task<BalanceInquiryDto> GetBalance(long accountnumber)
        {
            var account = await _context.Accounts.Include(a => a.Customer).Include(a => a.Branch).FirstOrDefaultAsync(a => a.AccountNumber == accountnumber);

            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            return new BalanceInquiryDto
            {
                AccountNumber = account.AccountNumber,
                CustomerName = account.Customer.FirstName + " " + account.Customer.LastName,
                AccountType = account.AccountType ?? "",
                Balance = account.Balance ?? 0,
                Status = account.Status ?? "",
                BranchName = account.Branch.BranchName
            };
        }

        public async Task<WithdrawResponseDto> Withdraw(WithdrawDto dto)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(e => e.AccountNumber == dto.AccountNumber);

            if (account == null)
            {
                throw new Exception("Not found Account");
            }

            decimal PreviousBalance=account.Balance ?? 0;

            if (PreviousBalance < dto.Amount)
            {
                throw new Exception("Insufficent Balance");
            }

            account.Balance = PreviousBalance - dto.Amount;

            Transaction transaction = new Transaction
            {
                AccountId = account.AccountId,
                TransactionType = "Withdraw",
                Amount = dto.Amount,
                Description = dto.Description,
                ReferenceNumber = Guid.NewGuid().ToString(),
                TransactionDate = DateTime.Now
            };
            _context.Transactions.Add(transaction);
           await _context.SaveChangesAsync();

            return new WithdrawResponseDto
            {
                Message = "Amount Withdraw successfull",
                AccountNumber = account.AccountNumber,
                PreviousBalance = PreviousBalance,
                WithdrawAmount = dto.Amount,
                CurrentBalance = account.Balance ?? 0,
                TransactionDate = transaction.TransactionDate ?? DateTime.Now
            };
        }
    }
}
