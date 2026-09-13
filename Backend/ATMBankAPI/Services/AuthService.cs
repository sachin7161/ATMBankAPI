using ATMBankAPI.Dtos;
using ATMBankAPI.Helpers;
using ATMBankAPI.Interfaces;

namespace ATMBankAPI.Services
{
    public class AuthService : IAuthServices
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
           var user=await _authRepository.Login(dto);

            if(user == null)
            {
                return null;
            }

            string token=JwtTokenHelper.GenratedToken(user,_configuration);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"])),

                UserId = user.UserId,
                UserName = user.UserName,
                Role = user.Role.RoleName
            };
        }
    }
}
