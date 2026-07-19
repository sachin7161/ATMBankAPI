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

        public async Task<FundTransferResponseDto> FundTransfer(FundTransferDto dto)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var sender = await _context.Accounts.FirstOrDefaultAsync(e => e.AccountNumber == dto.FromAccountNumber);

                if (sender == null)
                {
                    throw new Exception("Sender Account Not Found");
                }

                var receiver = await _context.Accounts.FirstOrDefaultAsync(e => e.AccountNumber == dto.ToAccountNumber);
                if (receiver == null)
                {
                    throw new Exception("receiver Account Not Found ");
                }

                if (sender.AccountId == receiver.AccountId)
                {
                    throw new Exception("Sender and Receiver account cannot be same.");
                }

                decimal senderBalance = sender.Balance ?? 0;

                if (senderBalance < dto.Amount)
                {
                    throw new Exception("Insufficent Balance");
                }

                sender.Balance = senderBalance-dto.Amount;
                receiver.Balance = (receiver.Balance ?? 0) + dto.Amount;

                string referenceNumber = "TRF" + DateTime.Now.ToString("yyMMddHHmmss");

                Transaction senderTransaction = new Transaction
                {
                 AccountId = sender.AccountId,
                 TransactionType="Fund Transfer Debit",
                 Amount=dto.Amount,
                 Description=dto.Description,
                 ReferenceNumber=referenceNumber,
                 TransactionDate=DateTime.Now,

                };

                Transaction reciverTransaction = new Transaction
                {
                    AccountId = sender.AccountId,
                    TransactionType = "Fund Trnsafer  Credit",
                    Amount = dto.Amount,
                    Description = dto.Description,
                    ReferenceNumber = referenceNumber,
                    TransactionDate = DateTime.Now,

                };
                _context.Transactions.Add(senderTransaction);
                _context.Transactions.Add(reciverTransaction);

                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                FundTransferResponseDto FTD = new FundTransferResponseDto
                {
                    Message="Fund Transfer Successfully",
                    ReferanceNumber=referenceNumber,
                    FromAccountNumber=sender.AccountNumber,
                    ToAccountNumber=receiver.AccountNumber,
                    Amount=dto.Amount,
                    RemainingBalance=sender.Balance??0
                };
                return FTD;
            }
            catch(Exception )
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
          
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

        public async Task<List<MiniStatementDto>> GetMiniStatement(long accountnumber)
        {
            var account = await _context.Accounts
         .FirstOrDefaultAsync(a => a.AccountNumber == accountnumber);

            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            var transactions = await _context.Transactions
                .Where(t => t.AccountId == account.AccountId)
                .OrderByDescending(t => t.TransactionDate)
                .Take(10)
                .Select(t => new MiniStatementDto
                {
                    TransactionType = t.TransactionType ?? "",
                    Amount = t.Amount ?? 0,
                    Description = t.Description ?? "",
                    TransactionDate = t.TransactionDate ?? DateTime.Now
                })
                .ToListAsync();

            return transactions;
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
