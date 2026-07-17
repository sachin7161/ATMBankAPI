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
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("Login")]

        public async Task<IActionResult>Login(LoginRequestDto dto)
        {
            var user=await _authRepository.Login(dto);

            if(user == null)
            {
                return Unauthorized(new
                {
                    Message = "Invalid UserName or Password"
                });
            }

            return Ok(new
            {
                Message = "Login Successfull",
                UserId=user.UserId,
                UserName=user.UserName,
                Role=user.Role.RoleName
            });
        }
    }
}
