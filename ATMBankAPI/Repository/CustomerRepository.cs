using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ATMBankDbContext _context;
        public CustomerRepository(ATMBankDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddCustomer(CreateCustomerDto dto)
        {
            var customerCount = await _context.Customers.CountAsync();
            Customer customer = new Customer()
            {
                CustomerCode = "CUST" + (customerCount + 1).ToString("000"),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                Dob = dto.Dob,
                MobileNumber = dto.MobileNumber,
                Email = dto.Email,
                AadharNo = dto.AadharNo,
                PanNo = dto.PanNo,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                Pincode = dto.Pincode,
                IsActive = true,
                CreateDate = DateTime.Now
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<List<CustomerDto>> GetAllCustomers()
        {
            return await _context.Customers.Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                CustomerCode = c.CustomerCode,
                FirstName = c.FirstName,
                LastName = c.LastName,
                MobileNumber = c.MobileNumber,
                Email = c.Email,
                City = c.City,
                State = c.State,
            }).ToListAsync();
        }

        public async Task<CustomerDto> GetCustomerById(int id)
        {
            return await _context.Customers.Where(c => c.CustomerId == id).Select(c => new CustomerDto
            {
                CustomerId = c.CustomerId,
                CustomerCode = c.CustomerCode,
                FirstName = c.FirstName,
                LastName = c.LastName,
                MobileNumber = c.MobileNumber,
                Email = c.Email,
                City = c.City,
                State = c.State,
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> IsEmailExists(string Email)
        {
            return await _context.Customers.AnyAsync(x => x.Email == Email);
        }

        public async Task<bool> IsMobileExist(string MobileNumber)
        {
            return await _context.Customers.AnyAsync(x => x.MobileNumber == MobileNumber);
        }
    }
}
