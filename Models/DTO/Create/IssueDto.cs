namespace CompanyApp.Models.DTO.Create;

public class IssueDto
{
    public string IssueDate { get; set; } = null!;

    public int? RollNumberId { get; set; }   // Foreign Key

    public Int16 ProductStockId { get; set; }   // Foreign Key

    public string? RollNumber { get; set; } = null!;

    public Single Weight { get; set; }

    public Single? Moisture { get; set; }
}