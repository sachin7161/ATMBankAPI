using ATMBankAPI.Dtos;
using ATMBankAPI.Helpers;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;

namespace ATMBankAPI.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public RefreshTokenService(
            IRefreshTokenRepository refreshTokenRepository,
            IAuthRepository authRepository,
            IConfiguration configuration)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public async Task<RefreshTokenResponseDto> GenerateRefreshToken(int userId)
        {
            var refreshToken = Guid.NewGuid().ToString();

            var token = new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                ExpiryDate = DateTime.Now.AddDays(7),
                IsRevoked = false,
                CreateDate = DateTime.Now
            };

            await _refreshTokenRepository.AddRefreshToken(token);

            return new RefreshTokenResponseDto
            {
                RefreshToken = refreshToken,
                Expiration = token.ExpiryDate!.Value
            };
        }

        public async Task<RefreshTokenResponseDto> RefreshAccessToken(string refreshToken)
        {
            var storedToken =
                await _refreshTokenRepository.GetRefreshToken(refreshToken);

            if (storedToken == null)
            {
                throw new Exception("Invalid Refresh Token");
            }

            if (storedToken.IsRevoked == true)
            {
                throw new Exception("Refresh Token is revoked");
            }

            if (storedToken.ExpiryDate < DateTime.Now)
            {
                throw new Exception("Refresh Token is expired");
            }

            var user = storedToken.User;

            string accessToken =
                JwtTokenHelper.GenratedToken(user, _configuration);

            return new RefreshTokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiration = DateTime.Now.AddMinutes(
                    Convert.ToDouble(
                        _configuration["Jwt:ExpiryInMinutes"]))
            };
        }
    }
}
