using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class Issue
{
    [Column("Issue_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IssueId { get; set; }

    [Column("I_Date")]
    public DateOnly IssueDate { get; set; }

    [Column("RN_Id")]
    public int? RollNumberId { get; set; }   // Foreign Key

    [Column("PS_Id")]
    public Int16 ProductStockId { get; set; }   // Foreign Key

    [Column("Roll_No", TypeName = "varchar(100)")]
    public string? RollNumber { get; set; } = null!;

    public Single Weight { get; set; }

    public Single? Moisture { get; set; }

    // Navigation property
    public virtual RollNumber RollNumbers { get; set; } = null!;

    // Navigation property
    public virtual ProductStock ProductStocks { get; set; } = null!;

    // Navigation property
    public virtual ICollection<ProductionCoating> ProductionCoatings { get; set; } = null!;
}