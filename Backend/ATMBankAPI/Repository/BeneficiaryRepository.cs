using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace ATMBankAPI.Repository
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly ATMBankDbContext _context;
        public BeneficiaryRepository(ATMBankDbContext context)
        {
            _context = context;
        }
        public async Task<BeneficiaryResponseDto> AddBeneficiary(BeneficiaryDto dto)
        {
           var account=await _context.Accounts.FirstOrDefaultAsync(e=>e.AccountId==dto.AccountId);

            if (account == null)
            {
                throw new Exception("Account not Found");
            }

            var beneficiaryAccount = await _context.Accounts.FirstOrDefaultAsync(e => e.AccountNumber == dto.BeneficiaryAccount);
            if (beneficiaryAccount == null)
            {
                throw new Exception("Beneficiary Account Not Found");
            }

            //Cannot Add own Account

            if(account.AccountNumber == dto.BeneficiaryAccount)
            {
                throw new Exception("You cannot  add your own Account as Beneficiary ");
            }

            //Dublicate Beneficiary Account Check

            var aleradyExist=await _context.Beneficiaries.AnyAsync(a=>a.AccountId==dto.AccountId && a.BeneficiaryAccount==dto.BeneficiaryAccount);

            if (aleradyExist)
            {
                throw new Exception("Beneficiary alerady Exist");
            }

            var beneficiary = new Beneficiary
            {
                AccountId = dto.AccountId,
                BeneficiaryName=dto.BeneficiaryName,
                BeneficiaryAccount=dto.BeneficiaryAccount,
                Ifsccode=dto.Ifsccode,
                NickName=dto.NickName,
                CreateDate=DateTime.Now

            };
            _context.Beneficiaries.Add(beneficiary);
            await _context.SaveChangesAsync();
            return new BeneficiaryResponseDto
            {
                Message = "Beneficiary added successfully.",
                BeneficiaryId = beneficiary.BeneficiaryId,
                BeneficiaryName = beneficiary.BeneficiaryName ?? "",
                BeneficiaryAccount = beneficiary.BeneficiaryAccount ?? 0,
                NickName = beneficiary.NickName
            };
        }

        public async Task<string> DeleteBeneficiary(int beneficiaryId)
        {
            var beneficiary = await _context.Beneficiaries.FirstOrDefaultAsync(b => b.BeneficiaryId == beneficiaryId);

            if (beneficiary == null)
            {
                throw new Exception("Beneficiary not found.");
            }

            _context.Beneficiaries.Remove(beneficiary);

            await _context.SaveChangesAsync();

            return "Beneficiary deleted successfully.";
        }

        public async Task<List<BeneficiaryResponseDto>> GetAllBeneficiaries(int accountId)
        {
            var accountExists = await _context.Accounts
        .AnyAsync(a => a.AccountId == accountId);

            if (!accountExists)
            {
                throw new Exception("Account not found.");
            }

            var beneficiaries = await _context.Beneficiaries.Where(b => b.AccountId == accountId).OrderByDescending(b => b.CreateDate).Select(b => new BeneficiaryResponseDto
                {
                    Message = "Beneficiaries found successfully.",
                    BeneficiaryId = b.BeneficiaryId,
                    BeneficiaryName = b.BeneficiaryName ?? "",
                    BeneficiaryAccount = b.BeneficiaryAccount ?? 0,
                    NickName = b.NickName
                }).ToListAsync();

            return beneficiaries;
        }

        public async Task<BeneficiaryResponseDto> GetBeneficiary(int beneficiaryId)
        {
            var beneficiary = await _context.Beneficiaries
        .FirstOrDefaultAsync(b => b.BeneficiaryId == beneficiaryId);

            if (beneficiary == null)
            {
                throw new Exception("Beneficiary not found.");
            }

            return new BeneficiaryResponseDto
            {
                Message = "Beneficiary found successfully.",
                BeneficiaryId = beneficiary.BeneficiaryId,
                BeneficiaryName = beneficiary.BeneficiaryName ?? "",
                BeneficiaryAccount = beneficiary.BeneficiaryAccount ?? 0,
                NickName = beneficiary.NickName
            };
        }

        public async Task<BeneficiaryTransferResponseDto> TransferToBeneficiary(BeneficiaryTransferDto dto)
        {
            // Source Account
            var sourceAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == dto.AccountId);
            if (sourceAccount == null)
            {
                throw new Exception("Source Account Not Found");
            }

            // Account Status
            if (sourceAccount.Status != "Active")
            {
                throw new Exception("Source Account Not Found");
            }

            //Beneficiary
            var beneficiary=await _context.Beneficiaries.FirstOrDefaultAsync(e=>e.BeneficiaryId==dto.BeneficiaryId && e.AccountId==dto.AccountId);

            if (beneficiary == null)
            {
                throw new Exception("Beneficiary Not Found");
            }

            // Beneficiary Account
            var destinationAccount = await _context.Accounts.FirstOrDefaultAsync(e => e.AccountNumber == beneficiary.BeneficiaryAccount);
            if(destinationAccount == null)
            {
                throw new Exception("Beneficiary Account Not Found");
            }



            //Amount Validations

            if (dto.Amount <= 0)
            {
                throw new Exception("Transfer Amount Must Be Gater Than");
            }

            // Balance Validation

            var sourcebalance = sourceAccount.Balance ?? 0;
            if (sourcebalance < dto.Amount)
            {
                throw new Exception("Insufficent Balance");
            }

            // Prevent Self Transfer

            if (sourceAccount.AccountId == destinationAccount.AccountId)
            {
                throw new Exception("Cannot Transfer to your Own Account");
            }
            var transactionDate = DateTime.Now;

            var referenceNumber = "TRF" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //Previous Balance
            var previousBalance = sourcebalance;
            // Deduct From Source Account
            sourceAccount.Balance = sourcebalance - dto.Amount;

            //Credit Destination Account
            destinationAccount.Balance = (destinationAccount.Balance ?? 0) + dto.Amount;
            var debitTransaction = new Transaction
            {
                AccountId = sourceAccount.AccountId,
                TransactionType = "Transfer Debit",
                Amount = dto.Amount,
                Description = dto.Description ?? "Beneficiary Fund Transfer",
                ReferenceNumber = referenceNumber,
                TransactionDate = transactionDate
            };
          //  Destination Transaction
                var creditTransaction = new Transaction
                {
                    AccountId = destinationAccount.AccountId,
                    TransactionType = "Transfer Credit",
                    Amount = dto.Amount,
                    Description = "Received Fund Transfer",
                    ReferenceNumber = referenceNumber,
                    TransactionDate = transactionDate
                };

            _context.Transactions.Add(debitTransaction);
            _context.Transactions.Add(creditTransaction);
                 // Save
                await _context.SaveChangesAsync();

            // Response
            return new BeneficiaryTransferResponseDto
            {
                Message = "Fund Transfer Successfully.",
                FromAccountNumber = sourceAccount.AccountNumber,
                ToAccountNumber = destinationAccount.AccountNumber,
                TransferAmount = dto.Amount,
                PreviousBalance = previousBalance,
                CurrentBalance = sourceAccount.Balance ?? 0,
                ReferenceNumber = referenceNumber,
                TransactionDate = transactionDate
            };
        }
        public async Task<BeneficiaryResponseDto> UpdateBeneficiary(int beneficiaryId, BeneficiaryDto dto)
        {
            var beneficiary = await _context.Beneficiaries.FirstOrDefaultAsync(b => b.BeneficiaryId == beneficiaryId);

            if (beneficiary == null)
            {
                throw new Exception("Beneficiary not found.");
            }

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == beneficiary.AccountId);

            if (account == null)
            {
                throw new Exception("Account not found.");
            }

            var beneficiaryAccount = await _context.Accounts.FirstOrDefaultAsync(a =>
                    a.AccountNumber == dto.BeneficiaryAccount);

            if (beneficiaryAccount == null)
            {
                throw new Exception("Beneficiary account not found.");
            }

            if (account.AccountNumber == dto.BeneficiaryAccount)
            {
                throw new Exception(
                    "You cannot add your own account as beneficiary.");
            }

            var duplicate = await _context.Beneficiaries
                .AnyAsync(b =>
                    b.AccountId == beneficiary.AccountId &&
                    b.BeneficiaryAccount == dto.BeneficiaryAccount &&
                    b.BeneficiaryId != beneficiaryId);

            if (duplicate)
            {
                throw new Exception("Beneficiary already exists.");
            }

            beneficiary.BeneficiaryName = dto.BeneficiaryName;
            beneficiary.BeneficiaryAccount = dto.BeneficiaryAccount;
            beneficiary.Ifsccode = dto.Ifsccode;
            beneficiary.NickName = dto.NickName;

            await _context.SaveChangesAsync();

            return new BeneficiaryResponseDto
            {
                Message = "Beneficiary updated successfully.",
                BeneficiaryId = beneficiary.BeneficiaryId,
                BeneficiaryName = beneficiary.BeneficiaryName ?? "",
                BeneficiaryAccount = beneficiary.BeneficiaryAccount ?? 0,
                NickName = beneficiary.NickName
            };
        }

        public async Task<bool> IsAccountOwnedByUser(int accountId, int userId)
        {
            var result = await _context.Accounts
                .AnyAsync(a =>
                    a.AccountId == accountId &&
                    a.Customer.Users.Any(u => u.UserId == userId));

            return result;
        }

        public async Task<bool> IsBeneficiaryOwnedByUser(int beneficiaryId, int userId)
        {
            var result = await _context.Beneficiaries
                .AnyAsync(b =>
                    b.BeneficiaryId == beneficiaryId &&
                    b.Account.Customer.Users.Any(u => u.UserId == userId));

            return result;
        }
    }
}
