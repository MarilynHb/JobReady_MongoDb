using MongoDB.Bson;

public class JobPost{
    public ObjectId Id {get;set;}
    public required string Title {get;set;}
    public required string Description {get;set;}
    public JobType JobType {get;set;}
    public DateTime CreatedOn {get;set;}
    public IEnumerable<JobApplication> Applications { get;set;} = [];
}