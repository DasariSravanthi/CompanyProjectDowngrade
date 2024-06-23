namespace CompanyApp.Models.DTO.Create;

public class ProductionCalendaringDto
{
    public string ProductionCoatingDate { get; set; } = null!;

    public int ProductionCoatingId { get; set; }    // Foreign Key

    public string RollNumber { get; set; } = null!;

    public Single BeforeWeight { get; set; }

    public Single BeforeMoisture { get; set; }

    public string CalendaringStart { get; set; } = null!;

    public string CalendaringEnd { get; set; } = null!;

    public byte RollCount { get; set; }
}