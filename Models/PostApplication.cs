using MongoDB.Bson;

public class PostApplication{
    public ObjectId Id {get;set;}
    public required string LetterOfMotivation {get;set;}
    public required string ApplicantId {get;set;}
    public DateTime AppliedOn {get;set;}
}