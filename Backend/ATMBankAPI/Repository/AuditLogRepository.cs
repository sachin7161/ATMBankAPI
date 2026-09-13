using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using ATMBankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Repository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ATMBankDbContext _context;
        public AuditLogRepository(ATMBankDbContext context)
        {
            _context = context;
        }
        public async Task<AuditLogResponseDto> AddAuditLog(AuditLogResponseDto dto)
        {
            var auditLog = new AuditLog
            {
                UserId = dto.UserId,
                ActionName = dto.ActionName,
                TableName = dto.TableName,
                RecordId = dto.RecordId,
                ActionDate = dto.ActionDate == default
                   ? DateTime.Now
                   : dto.ActionDate,
                IpAddress = dto.IpAddress
            };

            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();

            return new AuditLogResponseDto
            {
                AuditId = auditLog.AuditId,
                UserId = auditLog.UserId,
                ActionName = auditLog.ActionName ?? "",
                TableName = auditLog.TableName ?? "",
                RecordId = auditLog.RecordId,
                ActionDate = auditLog.ActionDate ?? DateTime.Now,
                IpAddress = auditLog.IpAddress ?? ""
            };
        }

        public async Task<List<AuditLogResponseDto>> GetAllAuditLogs()
        {
            return await _context.AuditLogs
                .OrderByDescending(a => a.ActionDate)
                .Select(a => new AuditLogResponseDto
                {
                    AuditId = a.AuditId,
                    UserId = a.UserId,
                    ActionName = a.ActionName ?? "",
                    TableName = a.TableName ?? "",
                    RecordId = a.RecordId,
                    ActionDate = a.ActionDate ?? DateTime.MinValue,
                    IpAddress = a.IpAddress ?? ""
                })
                .ToListAsync();

        }

        public async Task<AuditLogResponseDto> GetAuditLogById(long auditId)
        {
            var auditLog = await _context.AuditLogs
              .FirstOrDefaultAsync(a => a.AuditId == auditId);

            if (auditLog == null)
            {
                throw new Exception("Audit Log Not Found");
            }

            return new AuditLogResponseDto
            {
                AuditId = auditLog.AuditId,
                UserId = auditLog.UserId,
                ActionName = auditLog.ActionName ?? "",
                TableName = auditLog.TableName ?? "",
                RecordId = auditLog.RecordId,
                ActionDate = auditLog.ActionDate ?? DateTime.MinValue,
                IpAddress = auditLog.IpAddress ?? ""
            };
        }

        public async Task<List<AuditLogResponseDto>> GetAuditLogsByUser(int userId)
        {
            var userExists = await _context.Users
               .AnyAsync(u => u.UserId == userId);

            if (!userExists)
            {
                throw new Exception("User Not Found");
            }

            return await _context.AuditLogs
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.ActionDate)
                .Select(a => new AuditLogResponseDto
                {
                    AuditId = a.AuditId,
                    UserId = a.UserId,
                    ActionName = a.ActionName ?? "",
                    TableName = a.TableName ?? "",
                    RecordId = a.RecordId,
                    ActionDate = a.ActionDate ?? DateTime.MinValue,
                    IpAddress = a.IpAddress ?? ""
                })
                .ToListAsync();
        }
    }
}
