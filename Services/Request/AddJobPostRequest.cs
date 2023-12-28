namespace JobReady.Services;

public class AddJobPostRequest
{
    public string OwnerId { get; set; } = string.Empty;
    public required string Title { get; set; }
    public required string Description { get; set; }
    public JobType JobType { get; set; }
    public DateTime CreatedOn { get; set; }
}
