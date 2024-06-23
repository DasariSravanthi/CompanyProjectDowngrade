namespace CompanyApp.Models.DTO.Update;

public class UpdateReceiptDetailDto
{
    public int? ReceiptId { get; set; }   // Foreign Key

    public Int16? ProductStockId { get; set; }   // Foreign Key

    public Single? Weight { get; set; }

    public Single? UnitRate { get; set; }

    public byte? RollCount { get; set; }
}