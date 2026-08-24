using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;

namespace ATMBankAPI.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;

        public AuditLogService(IAuditLogRepository auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public async Task<AuditLogResponseDto> AddAuditLog(AuditLogResponseDto dto)
        {
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
            return await _auditLogRepository.GetAuditLogsByUser(userId);
        }
    }
}
