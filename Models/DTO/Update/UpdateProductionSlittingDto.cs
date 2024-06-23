namespace CompanyApp.Models.DTO.Update;

public class UpdateProductionSlittingDto
{
    public string? ProductionCoatingDate { get; set; }

    public int? ProductionCalendaringId { get; set; }    // Foreign Key

    public string? RollNumber { get; set; }

    public Single? BeforeWeight { get; set; }

    public Single? BeforeMoisture { get; set; }

    public string? SlittingStart { get; set; }

    public string? SlittingEnd { get; set; }

    public byte? RollCount { get; set; }
}