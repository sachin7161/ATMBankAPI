using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtmCardController : ControllerBase
    {
        private readonly IAtmCardService _atmservice;
        public AtmCardController(IAtmCardService atmservice)
        {
            _atmservice = atmservice;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAtm(AtmCardDto dto)
        {
            var result= await _atmservice.CreateAtmCard(dto);
            return Ok(result);
        }
        [HttpPost("{accountnumber}")]

        public async Task<IActionResult> GetAtmCard(long accountnumber)
        {
            var result = await _atmservice.GetAtmCard(accountnumber);
            return Ok(result);
        }
    }
}
