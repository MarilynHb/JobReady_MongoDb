using MongoDB.Bson;

public class JobPost{
    public ObjectId Id {get;set;}
    public required string Title {get;set;}
    public required string Description {get;set;}
    public JobType JobType {get;set;}
    public bool IsActive {get;set;}
    public bool IsRemote {get;set;}
    public required string Location {get;set;}
    public DateTime CreatedOn {get;set;}
}