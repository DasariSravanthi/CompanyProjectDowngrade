namespace CompanyApp.Models.DTO.Create;

public class SlittingDetailDto
{
    public int ProductionSlittingId { get; set; }    // Foreign Key

    public string RollNumber { get; set; } = null!;

    public Single Weight { get; set; }

    public Single Moisture { get; set; }
}