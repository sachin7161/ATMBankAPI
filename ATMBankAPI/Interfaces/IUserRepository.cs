using ATMBankAPI.Dtos;
using ATMBankAPI.Models;

namespace ATMBankAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User>CreateUser(CreateUserDto userDto);
        Task<bool> IUserNameExists(string username);
    }
}
