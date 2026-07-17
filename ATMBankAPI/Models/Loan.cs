using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Models;

public partial class Loan
{
    [Key]
    public int LoanId { get; set; }

    public int CustomerId { get; set; }

    public int LoanType { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? LoanAmount { get; set; }

    [Column("EMI", TypeName = "decimal(18, 2)")]
    public decimal? Emi { get; set; }

    public int? DurationMonths { get; set; }

    [StringLength(30)]
    public string? LoanStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApplyDate { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Loans")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("LoanType")]
    [InverseProperty("Loans")]
    public virtual LoanType LoanTypeNavigation { get; set; } = null!;
}
