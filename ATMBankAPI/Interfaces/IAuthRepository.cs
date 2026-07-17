using ATMBankAPI.Dtos;
using ATMBankAPI.Models;

namespace ATMBankAPI.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> Login(LoginRequestDto dto);
    }
}
