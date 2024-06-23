namespace CompanyApp.Models.DTO.Update;

public class UpdateProductionCalendaringDto
{
    public string? ProductionCoatingDate { get; set; }

    public int? ProductionCoatingId { get; set; }    // Foreign Key

    public string? RollNumber { get; set; }

    public Single? BeforeWeight { get; set; }

    public Single? BeforeMoisture { get; set; }

    public string? CalendaringStart { get; set; }

    public string? CalendaringEnd { get; set; }

    public byte? RollCount { get; set; }
}