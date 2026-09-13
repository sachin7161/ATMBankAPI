using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;

namespace ATMBankAPI.Services
{
    public class AtmCardService : IAtmCardService
    {
        public readonly IAtmCardRepository _atmcardrepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AtmCardService(IAtmCardRepository atmcardrepository, IHttpContextAccessor httpContextAccessor)
        {
            _atmcardrepository = atmcardrepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CardResponseDto> CardBlock(CardStatusDto dto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
       .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _atmcardrepository.IsAccountOwnedByUser(
                dto.AccountNumber,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this account.");
            }
            return await _atmcardrepository.BlockCard(dto);
        }

        public async Task<CardResponseDto> CardUnblock(CardStatusDto dto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
        .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _atmcardrepository.IsAccountOwnedByUser(
                dto.AccountNumber,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this account.");
            }
            return await _atmcardrepository.UnBlock(dto);
        }

        public async Task<ChangePinResponseDto> ChangePin(ChangePinDto dto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
       .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _atmcardrepository.IsAccountOwnedByUser(
                dto.AccountNumber,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this account.");
            }

            return await _atmcardrepository.ChangePin(dto);  
        }

        public async Task<AtmCardResponseDto> CreateAtmCard(AtmCardDto atmCardDto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
       .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _atmcardrepository.IsAccountOwnedByUser(
                atmCardDto.AccountNumber,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this account.");
            }
            return  await _atmcardrepository.CreateAtmCard(atmCardDto);
        }

        public async Task<GetAtmCardDto> GetAtmCard(long accountNumber)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
        .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _atmcardrepository.IsAccountOwnedByUser(
                accountNumber,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this account.");
            }

            return await _atmcardrepository.GetAtmCard(accountNumber);
        }
    }
}
