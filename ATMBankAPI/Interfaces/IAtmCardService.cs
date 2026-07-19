using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IAtmCardService
    {
        Task<AtmCardResponseDto>CreateAtmCard(AtmCardDto atmCardDto);
    }
}
