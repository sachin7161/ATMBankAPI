using ATMBankAPI.Dtos;
using ATMBankAPI.Helpers;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace ATMBankAPI.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ATMBankDbContext _context;
        public AuthRepository(ATMBankDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Login(LoginRequestDto dto)
        {
            var user = await _context.Users.Include(e => e.Role).FirstOrDefaultAsync(e => e.UserName == dto.UserName && e.IsActive == true);
            
            if(user == null)
            {
                return null;
            }

            bool ispasswordvalid=PasswordHelper.VerifyPassword(dto.Password,user.PasswordHash);

            if (!ispasswordvalid)
            {
                return null;
            }
            user.LastLogin=DateTime.Now;

            await _context.SaveChangesAsync();
            return user;
        }
    }
}
