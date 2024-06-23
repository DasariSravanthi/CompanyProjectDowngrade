namespace CompanyApp.Models.DTO.Update;

public class UpdateReceiptDto
{
    public string? ReceiptDate { get; set; }

    public byte? SupplierId { get; set; }   // Foreign Key

    public string? BillNo { get; set; }

    public string? BillDate { get; set; }

    public double? BillValue { get; set; }
}