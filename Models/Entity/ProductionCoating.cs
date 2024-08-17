using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class ProductionCoating
{
    [Column("P_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductionCoatingId { get; set; }

    [Column("P_Date")]
    public DateOnly ProductionCoatingDate { get; set; }

    [Column("Issue_Id")]
    public int IssueId { get; set; }    // Foreign Key

    [Column("Coating_Start")]
    public TimeOnly CoatingStart { get; set; }

    [Column("Coating_End")]
    public TimeOnly CoatingEnd { get; set; }

    [Column("Avg_Speed")]
    public byte AverageSpeed { get; set; }

    [Column("Avg_Temperature")]
    public byte AverageTemperature { get; set; }

    [Column("GSM_Coated")]
    public byte GSMCoated { get; set; }

    [Column("Roll_Count")]
    public byte RollCount { get; set; }

    // Navigation property
    public virtual Issue Issues { get; set; } = null!;

    // Navigation property
    public virtual ICollection<ProductionCalendaring> ProductionCalendarings { get; set; } = null!;
}