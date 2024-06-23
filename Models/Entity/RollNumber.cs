using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class RollNumber
{
    [Column("RN_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RollNumberId { get; set; }

    [Column("RD_Id")]
    public int ReceiptDetailId { get; set; }    // Foreign Key

    [Column("Roll_Number", TypeName = "varchar(100)")]
    public string RollNumberValue { get; set; } = null!;

    // Navigation property
    public virtual ReceiptDetail ReceiptDetails { get; set; } = null!;

    // Navigation property
    public virtual ICollection<Issue> Issues { get; set; } = null!;
}