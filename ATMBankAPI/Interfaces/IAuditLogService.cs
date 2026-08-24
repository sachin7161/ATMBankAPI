using ATMBankAPI.Dtos;

namespace ATMBankAPI.Interfaces
{
    public interface IAuditLogService
    {
        Task<AuditLogResponseDto> AddAuditLog(AuditLogResponseDto dto);

        Task<List<AuditLogResponseDto>> GetAllAuditLogs();

        Task<AuditLogResponseDto> GetAuditLogById(long auditId);

        Task<List<AuditLogResponseDto>> GetAuditLogsByUser(int userId);
    }
}
