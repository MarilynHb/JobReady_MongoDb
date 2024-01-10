namespace JobReady.Services;

public class AddPostRequest
{
    public string Id { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string OwnerId { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
}
