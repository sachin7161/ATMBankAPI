using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Models;

public partial class LoanType
{
    [Key]
    public int LoneTypeId { get; set; }

    [StringLength(100)]
    public string? LoneTypeName { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? IntresteRate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MaxAmount { get; set; }

    [InverseProperty("LoanTypeNavigation")]
    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
