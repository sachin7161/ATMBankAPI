using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Repository
{
    public class AtmCardRepository : IAtmCardRepository
    {
        private readonly ATMBankDbContext _context;
        public AtmCardRepository(ATMBankDbContext context)
        {
            _context = context;
        }

        public async Task<ChangePinResponseDto> ChangePin(ChangePinDto dto)
        {
            var card = await _context.AtmCards.Include(a => a.Account).FirstOrDefaultAsync(a => a.Account.AccountNumber == dto.AccountNumber);
            if (card == null)
            {
                throw new Exception("Atm Card Not Found");
            }
            bool isPinValid = Helpers.PasswordHelper.VerifyPin(dto.OldPin, card.PinHash);
            if(!isPinValid)
            {
                throw new Exception("Old pin is Incorrect");
            }
            if (dto.OldPin == dto.NewPin)
            {
                throw new Exception("New PIN cannot be the same as Old PIN.");

            }

            card.PinHash = Helpers.PasswordHelper.NewHashPin(dto.NewPin);

            await _context.SaveChangesAsync();

            ChangePinResponseDto res = new ChangePinResponseDto()
            {
                Meassage = "Atm Pin change Successfully"
            };
           
            return res;

        }

        public async Task<AtmCardResponseDto> CreateAtmCard(AtmCardDto atm)
        {
           var account=await _context.Accounts.FirstOrDefaultAsync(e=>e.AccountNumber==atm.AccountNumber);
            if (account == null)
            {
                throw new Exception("Account Not Found");
            }

            var exestingcard = await _context.AtmCards.FirstOrDefaultAsync(e => e.AccountId == account.AccountId);
            if(exestingcard != null)
            {
                throw new Exception("Atm Card Already exiest for this Account");
            }

            Random random=new Random();
            long CardNumber = long.Parse(
                random.Next(4000, 4999).ToString()
                + random.Next(1000, 9999).ToString() 
                + random.Next(1000, 9999).ToString()
                + random.Next(1000, 9999).ToString());

            string cvv=random.Next( 100,999).ToString();

            string pinHash = Helpers.PasswordHelper.HashPin(atm.Pin);

            AtmCard atmCard = new AtmCard
            {
                AccountId = account.AccountId,
                CardNumber = CardNumber,
                Cvv = cvv,
                ExpiryDate = DateOnly.FromDateTime(DateTime.Today.AddYears(5)),
                PinHash = pinHash,
                DailyLimit = atm.DailyLimit,
                CardStatus = "Active",
                CreateDate = DateTime.Now
            };
            _context.AtmCards.Add(atmCard);
            await _context.SaveChangesAsync();

            AtmCardResponseDto atmCardResponse = new AtmCardResponseDto
            {
                Message = "Atm Card Create Successfully",
                CardNumber = CardNumber,
                CVV = cvv,
                ExpiryDate = atmCard.ExpiryDate ?? DateOnly.MinValue,
                DailyLimit = atmCard.DailyLimit ?? 0,
                CardStatus = atmCard.CardStatus,
            };
            return  atmCardResponse;
        }

        public async Task<GetAtmCardDto> GetAtmCard(long accountnumber)
        {
            var card = await _context.AtmCards.Include(a => a.Account).ThenInclude(a => a.Customer)
                .FirstOrDefaultAsync(a => a.Account.AccountNumber == accountnumber);
            if(card == null)
            {
                throw new Exception("Atm Card Not Found");
            }
            GetAtmCardDto atm = new GetAtmCardDto()
            {
                CardNumber = card.CardNumber ?? 0,
                AccountNumber = card.Account.AccountNumber,
                CustomerName = card.Account.Customer.FirstName + "" + card.Account.Customer.LastName,
                ExpirayDate = card.ExpiryDate ?? DateOnly.MinValue,
                DailyLimit = card.DailyLimit ?? 0,
                CardStatus = card.CardStatus
            };
            return atm;


        }
    }
}
