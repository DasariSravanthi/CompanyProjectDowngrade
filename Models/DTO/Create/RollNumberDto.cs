namespace CompanyApp.Models.DTO.Create;

public class RollNumberDto
{
    public int ReceiptDetailId { get; set; }    // Foreign Key

    public string RollNumberValue { get; set; } = null!;
}