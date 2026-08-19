using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService _beneficiaryService;

        public BeneficiaryController(IBeneficiaryService beneficiaryService)
        {
            _beneficiaryService = beneficiaryService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddBeneficiary(BeneficiaryDto dto)
        {
            var result = await _beneficiaryService.AddBeneficiary(dto);

            return Ok(result);
        }

        [HttpGet("Get/{beneficiaryId}")]
        public async Task<IActionResult> GetBeneficiary(int beneficiaryId)
        {
            var result = await _beneficiaryService
                .GetBeneficiary(beneficiaryId);

            return Ok(result);
        }

        [HttpGet("GetAll/{accountId}")]
        public async Task<IActionResult> GetAllBeneficiaries(int accountId)
        {
            var result = await _beneficiaryService.GetAllBeneficiaries(accountId);

            return Ok(result);
        }

        [HttpPut("Update/{beneficiaryId}")]
        public async Task<IActionResult> UpdateBeneficiary(int beneficiaryId,BeneficiaryDto dto)
        {
            var result = await _beneficiaryService.UpdateBeneficiary(beneficiaryId, dto);

            return Ok(result);
        }


        [HttpDelete("Delete/{beneficiaryId}")]
        public async Task<IActionResult> DeleteBeneficiary(int beneficiaryId)
        {
            var result = await _beneficiaryService.DeleteBeneficiary(beneficiaryId);

            return Ok(new
            {
                message = result
            });
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> TransferToBeneficiary(BeneficiaryTransferDto dto)
        {
            var result = await _beneficiaryService.TransferToBeneficiary(dto);

            return Ok(result);
        }
    }
}
