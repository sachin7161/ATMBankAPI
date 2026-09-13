using ATMBankAPI.Models;

namespace ATMBankAPI.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetRefreshToken(string token);
        Task AddRefreshToken(RefreshToken refreshToken);
        Task RevokeRefreshToken(RefreshToken refreshToken);
    }
}
