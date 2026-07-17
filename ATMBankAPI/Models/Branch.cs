using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Models;

[Index("Ifsccode", Name = "UQ__Branches__6C74377FB42936BD", IsUnique = true)]
public partial class Branch
{
    [Key]
    public int BranchId { get; set; }

    [StringLength(100)]
    public string BranchName { get; set; } = null!;

    [StringLength(20)]
    public string? BranchCode { get; set; }

    [Column("IFSCCode")]
    [StringLength(20)]
    public string? Ifsccode { get; set; }

    [StringLength(300)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? State { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PinCode { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [InverseProperty("Branch")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
