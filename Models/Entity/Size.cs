using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class Size
{
    [Column("Size_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte SizeId { get; set; }

    [Column("Size")]
    public Int16 SizeInMM { get; set; }

    // Navigation property
    public virtual ICollection<ProductStock> ProductStocks { get; set; } = null!;
}