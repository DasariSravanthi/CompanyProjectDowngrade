namespace CompanyApp.Models.DTO.Update;

public class UpdateSlittingDetailDto
{
    public int? ProductionSlittingId { get; set; }    // Foreign Key

    public string? RollNumber { get; set; }

    public Single? Weight { get; set; }

    public Single? Moisture { get; set; }
}