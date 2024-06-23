using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class ProductStock
{
    [Column("PS_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int16 ProductStockId { get; set; }

    [Column("PD_Id")]
    public byte ProductDetailId { get; set; }   // Foreign Key

    public byte? GSM { get; set; }

    [Column("Size_Id")]
    public byte? SizeId { get; set; }   // Foreign Key

    [Column("Weight")]
    public Int16 WeightInKgs { get; set; }

    [Column("Roll_Count")]
    public byte? RollCount { get; set; }

    // Navigation property
    public virtual ProductDetail ProductDetails { get; set; } = null!;

    // Navigation property
    public virtual Size Sizes { get; set; } = null!;

    // Navigation property
    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = null!;

    // Navigation property
    public virtual ICollection<Issue> Issues { get; set; } = null!;
}