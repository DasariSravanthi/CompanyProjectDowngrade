namespace CompanyApp.Models.DTO.Create;

public class ReceiptDto
{
    public string ReceiptDate { get; set; } = null!;

    public byte SupplierId { get; set; }   // Foreign Key

    public string BillNo { get; set; } = null!;

    public string BillDate { get; set; } = null!;

    public double BillValue { get; set; }
}