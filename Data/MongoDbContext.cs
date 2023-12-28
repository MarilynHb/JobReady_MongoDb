using JobReady.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class MongoDbContext
{
    private readonly IMongoDatabase database;
    public MongoDbContext(IOptions<MongoDbConfiguration> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        database = client.GetDatabase(settings.Value.DatabaseName);
    }

     public IMongoCollection<User> UserCollection => database.GetCollection<User>("User");
     public IMongoCollection<University> UniversityCollection => database.GetCollection<University>("University");
     public IMongoCollection<Post> PostCollection => database.GetCollection<Post>("Post");
     public IMongoCollection<JobPost> JobPostCollection => database.GetCollection<JobPost>("JobPost");
}