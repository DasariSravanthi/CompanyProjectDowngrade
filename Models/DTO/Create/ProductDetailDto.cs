namespace CompanyApp.Models.DTO.Create;

public class ProductDetailDto 
{
    public byte ProductId { get; set; }
    
    public string Variant { get; set; } = null!;
}