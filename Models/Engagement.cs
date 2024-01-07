using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Engagement{
    public IEnumerable<string> Likes {get;set;} = [];
    public IEnumerable<string> Comments {get;set;} = [];
}

public class Like{
    public ObjectId Id {get;set;}
    [BsonRepresentation(BsonType.ObjectId)]
    public required string OwnerId {get;set;} 
    public DateTime CreatedOn {get;set;}
}

public class Comment {
    public ObjectId Id {get;set;}
    public required string Content {get;set;}
    [BsonRepresentation(BsonType.ObjectId)]
    public required string OwnerId {get;set;} 
    public DateTime CreatedOn {get;set;}   
}
