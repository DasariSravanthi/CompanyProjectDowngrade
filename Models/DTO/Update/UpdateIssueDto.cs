namespace CompanyApp.Models.DTO.Update;

public class UpdateIssueDto
{
    public string? IssueDate { get; set; }

    public int? RollNumberId { get; set; }   // Foreign Key

    public Int16? ProductStockId { get; set; }   // Foreign Key

    public string? RollNumber { get; set; }

    public Single? Weight { get; set; }

    public Single? Moisture { get; set; }
}