namespace CompanyApp.Models.DTO.Create;

public class ProductionSlittingDto
{
    public string ProductionCoatingDate { get; set; } = null!;

    public int ProductionCalendaringId { get; set; }    // Foreign Key

    public string RollNumber { get; set; } = null!;

    public Single BeforeWeight { get; set; }

    public Single BeforeMoisture { get; set; }

    public string SlittingStart { get; set; } = null!;

    public string SlittingEnd { get; set; } = null!;

    public byte RollCount { get; set; }
}