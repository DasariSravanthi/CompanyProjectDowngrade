using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class SlittingDetail
{
    [Column("SD_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SlittingDetailId { get; set; }

    [Column("PS_Id")]
    public int ProductionSlittingId { get; set; }    // Foreign Key

    [Column("Roll_No", TypeName = "varchar(100)")]
    public string RollNumber { get; set; } = null!;

    public Single Weight { get; set; }

    public Single Moisture { get; set; }

    // Navigation property
    public virtual ProductionSlitting ProductionSlittings { get; set; } = null!;
}