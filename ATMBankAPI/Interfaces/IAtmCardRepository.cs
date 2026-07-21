using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IAtmCardRepository
    {
        Task<AtmCardResponseDto> CreateAtmCard(AtmCardDto atm);
        Task<GetAtmCardDto> GetAtmCard(long accountnumber);
        
    }
}
