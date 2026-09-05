using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;

namespace ATMBankAPI.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuditLogService(IAuditLogRepository auditLogRepository, IHttpContextAccessor httpContextAccessor)
        {
            _auditLogRepository = auditLogRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuditLogResponseDto> AddAuditLog(AuditLogResponseDto dto)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User
        .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int userId = int.Parse(userIdClaim.Value);

            dto.UserId = userId;

            return await _auditLogRepository.AddAuditLog(dto);
        }

        public async Task<List<AuditLogResponseDto>> GetAllAuditLogs()
        {
            return await _auditLogRepository.GetAllAuditLogs();
        }

        public async Task<AuditLogResponseDto> GetAuditLogById(long auditId)
        {
            return await _auditLogRepository.GetAuditLogById(auditId);
        }

        public async Task<List<AuditLogResponseDto>> GetAuditLogsByUser(int userId)
        {
            var currentUserIdClaim = _httpContextAccessor.HttpContext?.User
        .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (currentUserIdClaim == null)
            {
                throw new Exception("User not authenticated");
            }

            int currentUserId = int.Parse(currentUserIdClaim.Value);

            bool isAdmin = _httpContextAccessor.HttpContext?.User
                .IsInRole("Admin") ?? false;

            if (!isAdmin && currentUserId != userId)
            {
                throw new Exception("You are not authorized to access these audit logs.");
            }
            return await _auditLogRepository.GetAuditLogsByUser(userId);
        }
    }
}
