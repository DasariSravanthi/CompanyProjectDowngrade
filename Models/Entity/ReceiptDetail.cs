using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class ReceiptDetail
{
    [Column("RD_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReceiptDetailId { get; set; }

    [Column("R_Id")]
    public int ReceiptId { get; set; }   // Foreign Key

    [Column("PS_Id")]
    public Int16 ProductStockId { get; set; }   // Foreign Key

    public Single Weight { get; set; }

    [Column("Unit_Rate")]
    public Single UnitRate { get; set; }

    [Column("Roll_Count")]
    public byte? RollCount { get; set; }

    // Navigation property
    public virtual Receipt Receipts { get; set; } = null!;

    // Navigation property
    public virtual ProductStock ProductStocks { get; set; } = null!;

    // Navigation property
    public virtual ICollection<RollNumber> RollNumbers { get; set; } = null!;
}