using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Models;

public partial class AuditLog
{
    [Key]
    public long AuditId { get; set; }

    public int? UserId { get; set; }

    [StringLength(200)]
    public string? ActionName { get; set; }

    [StringLength(100)]
    public string? TableName { get; set; }

    public int? RecordId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActionDate { get; set; }

    [StringLength(100)]
    public string? IpAddress { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AuditLogs")]
    public virtual User? User { get; set; }
}
