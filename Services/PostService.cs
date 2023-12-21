using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class PostService : IPostService
{
    private readonly IMongoCollection<Post> postCollection;
    public PostService(IOptions<MongoDbConfiguration> settings){
        var context = new MongoDbContext(settings);
        postCollection = context.PostCollection ;
    }
    public async Task<List<Post>> GetAllAsync(){
        return await postCollection.Find(c => true).ToListAsync();
    }
    public async Task<Post> GetByIdAsync(string id){
        return await postCollection.Find<Post>(c => c.Id.ToString() == id).FirstOrDefaultAsync();
    }
    public async Task<Post> CreateAsync(Post post){
        await postCollection.InsertOneAsync(post);
        return post;
    }
    public async Task UpdateAsync(string id, Post post){
        await postCollection.ReplaceOneAsync(c => c.Id.ToString() == id, post);
    }
    public async Task DeleteAsync(string id){
        await postCollection.DeleteOneAsync(c => c.Id.ToString() == id);
    }
}