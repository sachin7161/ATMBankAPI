using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]

        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            if(await _userRepository.IUserNameExists(dto.UserName))
            {
                return BadRequest(
                    new
                    {
                        Message = "UserName already Exist"
                    });
            }
            var user=await _userRepository.CreateUser(dto);
            return Ok(user);
        }
        
    }
}
