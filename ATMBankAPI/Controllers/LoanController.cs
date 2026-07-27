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
    public class LoanController : ControllerBase
    {
        private readonly ILoanService loanService;
        public LoanController(ILoanService loanService)
        {
            this.loanService = loanService;
        }

        [HttpPost]
        public async Task<IActionResult>ApplyLoan(ApplyLoanDto dto)
        {
            var result=await loanService.ApplyLoan(dto);
            return Ok(result);

        }
        [HttpGet("{LoanId}")]
        public async Task<IActionResult>GetLoan(int LoanId)
        {
            var result=await loanService.GetLoan(LoanId);
            return Ok(result);
        }
    }
}
