using ATMBankAPI.Dtos;
using ATMBankAPI.Models;

namespace ATMBankAPI.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetAllCustomers();
        Task<CustomerDto> GetCustomerById(int id);
        Task<Customer> AddCustomer(CreateCustomerDto dto);
        Task<bool> IsMobileExist(string MobileNumber);
        Task<bool> IsEmailExists(string Email);
    }
}
