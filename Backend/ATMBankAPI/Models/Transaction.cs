using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Models;

public partial class Transaction
{
    [Key]
    public int TransactionId { get; set; }

    public int? AccountId { get; set; }

    [StringLength(30)]
    public string? TransactionType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [StringLength(3000)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? ReferenceNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransactionDate { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Transactions")]
    public virtual Account? Account { get; set; }
}
