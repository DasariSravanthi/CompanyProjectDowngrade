namespace CompanyApp.Models.DTO.Update;

public class UpdateProductionCoatingDto
{
    public string? ProductionCoatingDate { get; set; }

    public int? IssueId { get; set; }    // Foreign Key

    public string? CoatingStart { get; set; }

    public string? CoatingEnd { get; set; }

    public byte? AverageSpeeed { get; set; }

    public byte? AverageTemperature { get; set; }

    public byte? GSMCoated { get; set; }

    public byte? RollCount { get; set; }
}