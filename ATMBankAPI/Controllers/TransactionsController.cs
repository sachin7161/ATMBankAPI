using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpPost("Deposit")]

       public async Task<IActionResult> Deposit(DepositDto dto)
        {
            var result=await _transactionService.Deposit(dto);
             return Ok(result);
        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult>Withdraw(WithdrawDto dto)
        {
            var result=await _transactionService.Withdraw(dto);
            return Ok( result);
        }

        [HttpGet("Balance{accountnumber}")]

        public async Task<IActionResult>GetBalance(long accountnumber)
        {
            var result=await _transactionService.GetBalance(accountnumber);
            return Ok( result);
        }

        [HttpPost("MiniStatement{accountnumber}")]
        public async Task<IActionResult>GetMiniStatement(long accountnumber)
        {
            var result =await _transactionService.GetMiniStatement(accountnumber);
            return Ok(result);
        }
    }
}
