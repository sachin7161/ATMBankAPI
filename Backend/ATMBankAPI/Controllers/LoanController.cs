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

        [HttpPost("ApplyLoan")]
        public async Task<IActionResult> ApplyLoan(ApplyLoanDto dto)
        {
            var result = await loanService.ApplyLoan(dto);
            return Ok(result);

        }
        [HttpGet("{LoanId}")]
        public async Task<IActionResult> GetLoan(int LoanId)
        {
            var result = await loanService.GetLoan(LoanId);
            return Ok(result);
        }



        [HttpPost("ApprovedLoan")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApprovedLoan(UpdateLoanStatusDto dto)
        {
            var result = await loanService.ApproveLoan(dto);
            return Ok(result);
        }

        [HttpPost("RejectedLoan")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectedLoan(UpdateLoanStatusDto dto)
        {
            var result = await loanService.RejectLoan(dto);
            return Ok(result);
        }

        [HttpGet("GetLoanHistory/{customerId}")]
        public async Task<List<LoanHistoryDto>> GetLoanHistory(int customerId)
        {
            var result = await loanService.GetLoanHistory(customerId);
            return result;
        }
    }
}
