namespace CompanyApp.Models.DTO.Update;

public class UpdateRollNumberDto
{
    public int? ReceiptDetailId { get; set; }    // Foreign Key

    public string? RollNumberValue { get; set; }
}