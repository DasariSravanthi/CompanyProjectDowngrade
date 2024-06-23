using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class ProductDetail
{
    [Column("PD_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte ProductDetailId { get; set; }

    [Column("P_Id")]
    public byte ProductId { get; set; }   // Foreign Key

    [Column(TypeName = "varchar(100)")]
    public string Variant { get; set; } = null!;

    // Navigation property
    public virtual Product Products { get; set; } = null!;

    // Navigation property
    public virtual ICollection<ProductStock> ProductStocks { get; set; } = null!;
}
