using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMBankAPI.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountserrvice;
        public AccountsController(IAccountService accountserrvice)
        {
            _accountserrvice=accountserrvice;
        }

        [HttpPost]

        public async Task<IActionResult>CreateAccount(AccountDto dto)
        {
            var result= await _accountserrvice.CreateAccount(dto);
            return Ok(result);
        }

        
    }
}
