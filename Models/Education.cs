public class Education{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public required string University { get; set; } 
    public string Major { get; set; } = string.Empty;
    public string Degree { get; set; } = string.Empty;
}