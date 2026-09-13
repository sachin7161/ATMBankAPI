using ATMBankAPI.Dtos;
using ATMBankAPI.Models;

namespace ATMBankAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User>CreateUser(UserDto userDto);
        Task<bool> IUserNameExists(string username);
    }
}
