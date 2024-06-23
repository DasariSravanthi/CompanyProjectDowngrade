using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class ProductionSlitting
{
    [Column("PS_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductionSlittingId { get; set; }

    [Column("P_Date")]
    public DateOnly ProductionCoatingDate { get; set; }

    [Column("PC_Id")]
    public int ProductionCalendaringId { get; set; }    // Foreign Key

    [Column("Roll_No", TypeName = "varchar(100)")]
    public string RollNumber { get; set; } = null!;

    [Column("Before_Weight")]
    public Single BeforeWeight { get; set; }

    [Column("Before_Moisture")]
    public Single BeforeMoisture { get; set; }

    [Column("Slitting_Start")]
    public TimeOnly SlittingStart { get; set; }

    [Column("Slitting_End")]
    public TimeOnly SlittingEnd { get; set; }

    [Column("Roll_Count")]
    public byte RollCount { get; set; }

    // Navigation property
    public virtual ProductionCalendaring ProductionCalendarings { get; set; } = null!;

    // Navigation property
    public virtual ICollection<SlittingDetail> SlittingDetails { get; set; } = null!;
}