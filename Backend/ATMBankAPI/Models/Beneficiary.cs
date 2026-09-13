using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Models;

public partial class Beneficiary
{
    [Key]
    public int BeneficiaryId { get; set; }

    public int AccountId { get; set; }

    [StringLength(100)]
    public string? BeneficiaryName { get; set; }

    public long? BeneficiaryAccount { get; set; }

    [Column("IFSCCode")]
    [StringLength(20)]
    public string? Ifsccode { get; set; }

    [StringLength(50)]
    public string? NickName { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Beneficiaries")]
    public virtual Account Account { get; set; } = null!;
}
