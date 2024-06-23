namespace CompanyApp.Models.DTO.Update;

public class UpdateProductStockDto
{
    public byte? ProductDetailId { get; set; }   // Foreign Key

    public byte? GSM { get; set; }

    public byte? SizeId { get; set; }   // Foreign Key

    public Int16? WeightInKgs { get; set; }

    public byte? RollCount { get; set; }
}