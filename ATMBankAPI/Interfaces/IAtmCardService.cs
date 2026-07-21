using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IAtmCardService
    {
        Task<AtmCardResponseDto>CreateAtmCard(AtmCardDto atmCardDto);
        Task<GetAtmCardDto> GetAtmCard(long accountNumber);
        Task<ChangePinResponseDto> ChangePin(ChangePinDto dto);
        Task<CardResponseDto>CardBlock(CardStatusDto dto);
        Task<CardResponseDto>CardUnblock(CardStatusDto dto);
    }
}
