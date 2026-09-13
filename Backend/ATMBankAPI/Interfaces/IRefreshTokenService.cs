using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<RefreshTokenResponseDto> GenerateRefreshToken(int userId);
        Task<RefreshTokenResponseDto> RefreshAccessToken(string refreshToken);
    }
}
