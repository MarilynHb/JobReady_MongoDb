using MongoDB.Bson;

public class Post{
    public ObjectId Id {get;set;}
    public required string OwnerId {get;set;}
    public required string Content {get;set;} 
    public DateTime CreatedOn {get;set;}
    public Engagement Engagement {get;set;} = new();
}