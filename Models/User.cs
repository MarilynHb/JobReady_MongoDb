using MongoDB.Bson;

public class User {
    public ObjectId Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public AccountType Type { get; set; }
    public GenderType Gender { get; set; }
    public Industry Industry { get; set; }
    public IEnumerable<Education> Education {get;set;} = [];
    public IEnumerable<string> Post {get;set;} = [];
    public IEnumerable<string> JobPost {get;set;} = [];
}