using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyApp.Models.Entity;

public class Supplier
{
    [Column("S_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public byte SupplierId { get; set; }

    [Column("Supplier_Name", TypeName = "varchar(100)")]
    public string SupplierName { get; set; } = null!;

    public double Dues { get; set; }

    // Navigation property
    public virtual ICollection<Receipt> Receipts { get; set; } = null!;
}