using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ATMBankAPI.Models;

[Index("CustomerCode", Name = "UQ__Customer__06678521BD8349B7", IsUnique = true)]
[Index("MobileNumber", Name = "UQ__Customer__250375B1E86F33AE", IsUnique = true)]
[Index("AadharNo", Name = "UQ__Customer__40DC83D1DAFB0B47", IsUnique = true)]
[Index("Email", Name = "UQ__Customer__A9D10534193C1DD5", IsUnique = true)]
[Index("PanNo", Name = "UQ__Customer__F255F5837DC14282", IsUnique = true)]
public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? CustomerCode { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(10)]
    public string? Gender { get; set; }

    [Column("DOB")]
    public DateOnly? Dob { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string MobileNumber { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(12)]
    [Unicode(false)]
    public string? AadharNo { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? PanNo { get; set; }

    [StringLength(300)]
    public string? Address { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? City { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? State { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? Pincode { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("Customer")]
    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    [InverseProperty("Customer")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
