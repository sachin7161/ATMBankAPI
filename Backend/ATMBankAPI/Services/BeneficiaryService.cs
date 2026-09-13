using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Repository;
namespace ATMBankAPI.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiary;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _beneficiary = beneficiaryRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<BeneficiaryResponseDto> AddBeneficiary(BeneficiaryDto dto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _beneficiary.IsAccountOwnedByUser(
                dto.AccountId,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this account.");
            }
            return await _beneficiary.AddBeneficiary(dto);
        }

        public async Task<string> DeleteBeneficiary(int beneficiaryId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
       .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _beneficiary.IsBeneficiaryOwnedByUser(
                beneficiaryId,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this beneficiary.");
            }
            return await _beneficiary.DeleteBeneficiary(beneficiaryId);
        }

        public async Task<List<BeneficiaryResponseDto>> GetAllBeneficiaries(int accountId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _beneficiary.IsAccountOwnedByUser(
                accountId,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this account.");
            }
            return await _beneficiary.GetAllBeneficiaries(accountId);
        }

        public async Task<BeneficiaryResponseDto> GetBeneficiary(int beneficiaryId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
       .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _beneficiary.IsBeneficiaryOwnedByUser(
                beneficiaryId,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this beneficiary.");
            }
            return await _beneficiary.GetBeneficiary(beneficiaryId);
        }

        public async Task<BeneficiaryTransferResponseDto> TransferToBeneficiary(BeneficiaryTransferDto dto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
       .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _beneficiary.IsAccountOwnedByUser(
                dto.AccountId,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this account.");
            }
            return await _beneficiary.TransferToBeneficiary(dto);
        }

        public async Task<BeneficiaryResponseDto> UpdateBeneficiary(int beneficiaryId, BeneficiaryDto dto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
        .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _beneficiary.IsBeneficiaryOwnedByUser(
                beneficiaryId,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this beneficiary.");
            }
            return await _beneficiary.UpdateBeneficiary(beneficiaryId, dto);
        }
    }
}
