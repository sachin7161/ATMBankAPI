using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ATMBankDbContext _context;

        public RefreshTokenRepository(ATMBankDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetRefreshToken(string token)
        {
            return await _context.RefreshTokens.Include(x => x.User).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task AddRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task RevokeRefreshToken(RefreshToken refreshToken)
        {
            refreshToken.IsRevoked = true;
            await _context.SaveChangesAsync();
        }
    }
}
