using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

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


        [HttpPost("ChangeAtmPin")]
        public async Task<IActionResult>ChangeAtmPin(ChangePinDto dto)
        {
            var result = await _atmservice.ChangePin(dto);
            return Ok(result);
        }

        [HttpPost("CardBlock")]
        public async Task<IActionResult>CardBlock(CardStatusDto dto)
        {
            var result=await _atmservice.CardBlock(dto);
            return Ok(result);
        }
        [HttpPost("UnBlockCard")]
        public async Task<IActionResult>UnBlockCard(CardStatusDto dto)
        {
            var result=await _atmservice.CardUnblock(dto);
            return Ok(result);
        }
    }
}
