using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;

namespace ATMBankAPI.Services
{
    public class AtmCardService : IAtmCardService
    {
        public readonly IAtmCardRepository _atmcardrepository;
        public AtmCardService(IAtmCardRepository atmcardrepository)
        {
            _atmcardrepository = atmcardrepository;
        }

        public async Task<AtmCardResponseDto> CreateAtmCard(AtmCardDto atmCardDto)
        {
            return  await _atmcardrepository.CreateAtmCard(atmCardDto);
        }

        public async Task<GetAtmCardDto> GetAtmCard(long accountNumber)
        {
           return await _atmcardrepository.GetAtmCard(accountNumber);
        }
    }
}
