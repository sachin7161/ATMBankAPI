using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;
        public AuthController(IAuthServices authServices)
        {
            _authService = authServices;
        }

        [HttpPost("Login")]

        public async Task<IActionResult>Login(LoginRequestDto dto)
        {
            var response=await _authService.Login(dto); 


            if(response == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid UserName or Password"
                });
            }

            return Ok(response);
        }
    }
}
