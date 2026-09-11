using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ATMBankDbContext _context;
        public AccountRepository(ATMBankDbContext context)
        {
            _context = context;
        }

        public async Task<AccountResponseDto> CreateAccount(AccountDto dto)
        {
            //Genrated Account Number

            long accountNumber = 1000000001;
            var lastAccount= await _context.Accounts.OrderByDescending(e=>e.AccountNumber).FirstOrDefaultAsync();

            if(lastAccount != null)
            {
                accountNumber=lastAccount.AccountNumber+1;
            }

            //Get Customer

             var customer=await _context.Customers.FirstOrDefaultAsync(e=>e.CustomerId==dto.CustomerId);

            if(customer== null)
            {
                throw new Exception("Customer Not Found");
            }

            // Create Account

            Account account = new Account();

            account.CustomerId=dto.CustomerId;
            account.BranchId = dto.BranchId;
            account.AccountNumber=accountNumber;
            account.AccountType = dto.AccountType;
            account.Balance = dto.OpningBalance;
            account.Status = "Active";
            account.OpenDate = DateTime.Now;

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return new AccountResponseDto
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                CustomerName = customer.FirstName + " " + customer.LastName,
                AccountType = account.AccountType,
                Balance = account.Balance ?? 0,
                Status = account.Status,
                OpenDate = account.OpenDate

            };
        }

        public async Task<AccountResponseDto> GetAccountByNumber(long accountNumber)
        {
            var account = await _context.Accounts
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                throw new Exception("Account Not Found");
            }

            return new AccountResponseDto
            {
                AccountId = account.AccountId,
                AccountNumber = account.AccountNumber,
                CustomerName = account.Customer.FirstName + " " + account.Customer.LastName,
                AccountType = account.AccountType,
                Balance = account.Balance ?? 0,
                Status = account.Status,
                OpenDate = account.OpenDate
            };
        }

        public async Task<bool> IsAccountOwnedByUser(long accountNumber,int userId)
        {
            return await _context.Accounts.AnyAsync(a =>a.AccountNumber == accountNumber &&a.Customer.Users.Any(u => u.UserId == userId));
        }

        public async Task<AccountDashboardDto> GetAccountDashboard(long accountNumber)
        {
            var account = await _context.Accounts
                .Include(a => a.Customer)
                .Include(a => a.AtmCards)
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                throw new Exception("Account Not Found");
            }

            var customerLoans = await _context.Loans
                .Include(l => l.LoanTypeNavigation)
                .Where(l => l.CustomerId == account.CustomerId)
                .OrderByDescending(l => l.ApplyDate)
                .ToListAsync();

            var atmCard = account.AtmCards.FirstOrDefault();

            var dashboard = new AccountDashboardDto
            {
                Account = new AccountResponseDto
                {
                    AccountId = account.AccountId,
                    AccountNumber = account.AccountNumber,
                    CustomerName = account.Customer.FirstName + " " + account.Customer.LastName,
                    AccountType = account.AccountType ?? "",
                    Balance = account.Balance ?? 0,
                    Status = account.Status ?? "",
                    OpenDate = account.OpenDate
                },

                Customer = new CustomerDashboardDto
                {
                    CustomerId = account.Customer.CustomerId,
                    CustomerName = account.Customer.FirstName + " " + account.Customer.LastName,
                    MobileNumber = account.Customer.MobileNumber,
                    Email = account.Customer.Email,
                    Address = account.Customer.Address
                },

                AtmCard = atmCard == null ? null : new AtmCardDashboardDto
                {
                    CardNumber = atmCard.CardNumber ?? 0,
                    ExpiryDate = atmCard.ExpiryDate ?? DateOnly.MinValue,
                    DailyLimit = atmCard.DailyLimit ?? 0,
                    CardStatus = atmCard.CardStatus
                },

                Balance = account.Balance ?? 0,

                RecentTransactions = account.Transactions
                    .OrderByDescending(t => t.TransactionDate)
                    .Take(5)
                    .Select(t => new TransactionResponseDto
                    {
                        TransactionId = t.TransactionId,
                        AccountNumber = account.AccountNumber,
                        TransactionType = t.TransactionType ?? "",
                        Amount = t.Amount ?? 0,
                        Description = t.Description ?? "",
                        ReferenceNumber = t.ReferenceNumber ?? "",
                        TransactionDate = t.TransactionDate ?? DateTime.MinValue
                    })
                    .ToList(),

                Loans = customerLoans
                    .Select(l => new LoanHistoryDto
                    {
                        LoanId = l.LoanId,
                        LoanType = l.LoanType.ToString(),
                        LoanAmount = l.LoanAmount ?? 0,
                        Emi = l.Emi ?? 0,
                        DurationMonth = l.DurationMonths ?? 0,
                        LoanStatus = l.LoanStatus ?? "",
                        ApplyDate = l.ApplyDate ?? DateTime.MinValue
                    })
                    .ToList()
            };

            return dashboard;
        }
    }
}
