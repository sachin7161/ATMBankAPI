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
    }
}
