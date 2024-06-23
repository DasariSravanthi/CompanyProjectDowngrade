using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class ProductionCalendaring
{
    [Column("PC_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductionCalendaringId { get; set; }

    [Column("P_Date")]
    public DateOnly ProductionCoatingDate { get; set; }

    [Column("P_Id")]
    public int ProductionCoatingId { get; set; }    // Foreign Key

    [Column("Roll_No", TypeName = "varchar(100)")]
    public string RollNumber { get; set; } = null!;

    [Column("Before_Weight")]
    public Single BeforeWeight { get; set; }

    [Column("Before_Moisture")]
    public Single BeforeMoisture { get; set; }

    [Column("Calendaring_Start")]
    public TimeOnly CalendaringStart { get; set; }

    [Column("Calendaring_End")]
    public TimeOnly CalendaringEnd { get; set; }

    [Column("Roll_Count")]
    public byte RollCount { get; set; }

    // Navigation property
    public virtual ProductionCoating ProductionCoatings { get; set; } = null!;

    // Navigation property
    public virtual ICollection<ProductionSlitting> ProductionSlittings { get; set; } = null!;
}