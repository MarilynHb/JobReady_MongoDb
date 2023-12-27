public class Education
{
    public required string UniversityId { get; set; } 
    public string Major { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}