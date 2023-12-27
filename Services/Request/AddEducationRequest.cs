namespace JobReady.Services;

public class AddEducationRequest
{
    public required string OwnerId { get; set; }
    public int Index { get; set; }
    public required string UniversityId { get; set; }
    public required string Major { get; set; }
    public required string Degree { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
