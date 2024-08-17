namespace CompanyApp.Models.DTO.Create;

public class ProductionCoatingDto
{
    public string ProductionCoatingDate { get; set; } = null!;

    public int IssueId { get; set; }    // Foreign Key

    public string CoatingStart { get; set; } = null!;

    public string CoatingEnd { get; set; } = null!;

    public byte AverageSpeed { get; set; }

    public byte AverageTemperature { get; set; }

    public byte GSMCoated { get; set; }

    public byte RollCount { get; set; }
}