using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Models;

[Index("AccountNumber", Name = "UQ__Accounts__BE2ACD6F9C344610", IsUnique = true)]
public partial class Account
{
    [Key]
    public int AccountId { get; set; }

    public int CustomerId { get; set; }

    public int BranchId { get; set; }

    public long AccountNumber { get; set; }

    [StringLength(30)]
    public string? AccountType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Balance { get; set; }

    [StringLength(20)]
    public string? Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OpenDate { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<AtmCard> AtmCards { get; set; } = new List<AtmCard>();

    [InverseProperty("Account")]
    public virtual ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();

    [ForeignKey("BranchId")]
    [InverseProperty("Accounts")]
    public virtual Branch Branch { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("Accounts")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Account")]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
