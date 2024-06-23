using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class Product
{
    [Column("P_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte ProductId { get; set; }

    [Column("Product_Category", TypeName = "varchar(100)")]
    public string ProductCategory { get; set; } = null!;

    // Navigation property
    public virtual ICollection<ProductDetail> ProductDetails { get; set; } = null!;
}
