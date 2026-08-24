using ATMBankAPI.Dtos;
using ATMBankAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ATMBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAuditLog(
            AuditLogResponseDto dto)
        {
            var result = await _auditLogService.AddAuditLog(dto);

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAuditLogs()
        {
            var result = await _auditLogService.GetAllAuditLogs();

            return Ok(result);
        }

        [HttpGet("Get/{auditId}")]
        public async Task<IActionResult> GetAuditLogById(long auditId)
        {
            var result = await _auditLogService.GetAuditLogById(auditId);

            return Ok(result);
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetAuditLogsByUser(int userId)
        {
            var result = await _auditLogService.GetAuditLogsByUser(userId);

            return Ok(result);
        }
    }
}
