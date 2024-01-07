using MongoDB.Bson;

namespace JobReady.Models;

public class University{
    public ObjectId Id {get;set;}
    public required string Name {get;set;} 
    public required string Location {get;set;} 
}