using JobReady.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;

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
    public async Task<Post> CreateAsync(AddPostRequest request)
    {
        var post = new Post() { Content = request.Content, CreatedOn = DateTime.Now };
        await context.PostCollection.InsertOneAsync(post);
        await AddPostAsync(post.Id.ToString(), request.OwnerId);
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

    #region Get Post Owner Id
    public async Task<string> GetPostOwnerId(string postId)
    {
        var post = await context.UserCollection.Find(c => c.Post.Contains(postId)).FirstAsync();
        return post.Id.ToString();
    }
    #endregion

    public async Task<IEnumerable<User>> GetAllUser()
    {
        var userService = new UserService(this.settings);
        return await userService.GetAllAsync();
    }
}