namespace JobReady.Services;

public class EducationByUser
{
    public string FullName { get; set; } = string.Empty;
    public string OwnerId { get; set; } = string.Empty;
    public int Index { get; set; }
    public string University { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
