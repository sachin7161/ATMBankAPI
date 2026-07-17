using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IAuthServices
    {
        Task<LoginResponseDto> Login(LoginRequestDto dto);
    }
}
