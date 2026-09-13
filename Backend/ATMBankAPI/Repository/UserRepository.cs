using ATMBankAPI.Dtos;
using ATMBankAPI.Helpers;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ATMBankDbContext _context;
        public UserRepository(ATMBankDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUser(UserDto userDto)
        {
            User user = new User();
            user.UserName = userDto.UserName;
            user.PasswordHash=PasswordHelper.Hashpassword(userDto.Password);
            user.RoleId=userDto.RoleId;
            user.CustomerId=userDto.CustomerId;
            user.IsActive = true;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
          return user;
        }

        public async Task<bool> IUserNameExists(string username)
        {
            return await _context.Users.AnyAsync(e=>e.UserName==username);
        }
    }
}
