namespace ATMBankAPI.Dtos
{
    public class AuditLogResponseDto
    {
        public long AuditId { get; set; }

        public int? UserId { get; set; }

        public string ActionName { get; set; } = "";

        public string TableName { get; set; } = "";

        public int? RecordId { get; set; }

        public DateTime ActionDate { get; set; }

        public string IpAddress { get; set; } = "";
    }
}
