using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMBankAPI.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>CreateAccount(AccountDto dto)
        {
            var result= await _accountserrvice.CreateAccount(dto);
            return Ok(result);
        }

        [HttpGet("GetByNumber/{accountNumber}")]
        public async Task<IActionResult> GetAccountByNumber(long accountNumber)
        {
            var result = await _accountserrvice.GetAccountByNumber(accountNumber);

            return Ok(result);
        }
    }
}
