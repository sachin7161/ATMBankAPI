using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Models;

[Index("CardNumber", Name = "UQ__AtmCards__A4E9FFE9C461FDA3", IsUnique = true)]
public partial class AtmCard
{
    [Key]
    public int CardId { get; set; }

    [Column("AccountID")]
    public int AccountId { get; set; }

    public long? CardNumber { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string? Cvv { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string? PinHash { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? DailyLimit { get; set; }

    [StringLength(20)]
    public string? CardStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AtmCards")]
    public virtual Account Account { get; set; } = null!;
}
