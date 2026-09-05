using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
namespace ATMBankAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanService(ILoanRepository loanRepository, IHttpContextAccessor httpContextAccessor)
        {
            _loanRepository = loanRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApplyLoanResponseDto> ApplyLoan(ApplyLoanDto dto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _loanRepository.IsCustomerOwnedByUser(
                dto.CustomerId,
                userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this customer.");
            }
            return await _loanRepository.ApplyLoan(dto);
        }

        public Task<LoanStatusResponseDto> ApproveLoan(UpdateLoanStatusDto dto)
        {
            return _loanRepository.ApproveLoan(dto);
        }

        public  async Task<GetLoanDto> GetLoan(int loanId)
        {

            var userIdClaim = _httpContextAccessor.HttpContext?.User
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _loanRepository.IsLoanOwnedByUser(loanId,userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this loan.");
            }
            return await _loanRepository.GetLoan(loanId);
        }

        public async Task<List<LoanHistoryDto>> GetLoanHistory(int customerId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
        .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isOwned = await _loanRepository.IsCustomerOwnedByUser(customerId, userId);

            if (!isOwned)
            {
                throw new Exception("You are not authorized to access this customer.");
            }
            return await _loanRepository.GetLoanHistory(customerId);
        }

        public Task<LoanStatusResponseDto> RejectLoan(UpdateLoanStatusDto dto)
        {
            return _loanRepository.RejectLoan(dto);
        }
    }
}
