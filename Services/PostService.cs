using JobReady.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class PostService : IPostService
{
    private readonly MongoDbContext context;
    private readonly IOptions<MongoDbConfiguration> settings;   
    public PostService(IOptions<MongoDbConfiguration> settings)
    {
        this.settings = settings;
        context = new MongoDbContext(settings);
    }

    #region Get Post
    public async Task<List<Post>> GetAllAsync()
    {
        return await context.PostCollection.Find(c => true).ToListAsync();
    }
    public async Task<Post> GetByIdAsync(string id)
    {
        return await context.PostCollection.Find<Post>(c => c.Id.ToString() == id).FirstOrDefaultAsync();
    }
    #endregion

    #region Create Post
    public async Task<Post> CreateAsync(Post post)
    {
        await context.PostCollection.InsertOneAsync(post);
        await AddPostAsync(post.Id.ToString(), post.OwnerId);
        return post;
    }
    internal async Task AddPostAsync(string postId, string ownerId)
    {
        var userService = new UserService(this.settings);
        var user = await userService.GetByIdAsync(ownerId);
        user.Post = user.Post.Concat(new List<string> { postId }).ToList();
        await userService.UpdateAsync(ownerId, user);
    }
    #endregion

    #region Update Post
    public async Task UpdateAsync(string id, Post post)
    {
        await context.PostCollection.ReplaceOneAsync(c => c.Id.ToString() == id, post);
    }
    #endregion

    #region Delete Post
    public async Task DeleteAsync(string id)
    {
        await context.PostCollection.DeleteOneAsync(c => c.Id.ToString() == id);
    }
    #endregion

    public async Task<IEnumerable<User>> GetAllUser()
    {
        var userService = new UserService(this.settings);
        return await userService.GetAllAsync();
    }
}