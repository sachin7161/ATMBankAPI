using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerepository.GetAllCustomers();
            return Ok(customers);   
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCustomerById(int id)
        {
           var customer=await _customerepository.GetCustomerById(id);  
            if(customer == null)
            {
                return Ok("Customer not Found");
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult>CreateCustomer(CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(await _customerepository.IsMobileExist(dto.MobileNumber))
            {
                return BadRequest(new
                {
                    Message = "Mobile Number Already Exists"
                });
                if (!string.IsNullOrWhiteSpace(dto.Email))
                {
                    return BadRequest(new
                    {
                        Message = "Email Already Exists"
                    });
                }
            }
            var customer = await _customerepository.AddCustomer(dto);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }
    }
}
