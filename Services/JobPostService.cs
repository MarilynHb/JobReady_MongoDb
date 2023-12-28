using JobReady.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;

public class JobPostService : IJobPostService
{
    private readonly MongoDbContext context;
    private readonly IOptions<MongoDbConfiguration> settings;   
    public JobPostService(IOptions<MongoDbConfiguration> settings)
    {
        this.settings = settings;
        context = new MongoDbContext(settings);
    }

    #region Get Post
    public async Task<List<JobPost>> GetAllAsync()
    {
        return await context.JobPostCollection.Find(c => true).ToListAsync();
    }
    public async Task<JobPost> GetByIdAsync(string id)
    {
        return await context.JobPostCollection.Find<JobPost>(c => c.Id.ToString() == id).FirstOrDefaultAsync();
    }
    #endregion

    #region Create Post
    public async Task<JobPost> CreateAsync(AddJobPostRequest request)
    {
        var post = new JobPost() { Title = request.Title, Description = request.Description, JobType = request.JobType, CreatedOn = DateTime.Now };
        await context.JobPostCollection.InsertOneAsync(post);
        await AddJobPostAsync(post.Id.ToString(), request.OwnerId);
        return post;
    }
    internal async Task AddJobPostAsync(string postId, string ownerId)
    {
        var userService = new UserService(this.settings);
        var user = await userService.GetByIdAsync(ownerId);
        user.JobPost = user.JobPost.Concat(new List<string> { postId }).ToList();
        await userService.UpdateAsync(ownerId, user);
    }
    #endregion

    #region Update Post
    public async Task UpdateAsync(string id, JobPost post)
    {
        await context.JobPostCollection.ReplaceOneAsync(c => c.Id.ToString() == id, post);
    }
    #endregion

    #region Delete Post
    public async Task DeleteAsync(string id)
    {
        await context.JobPostCollection.DeleteOneAsync(c => c.Id.ToString() == id);
    }
    #endregion

    #region Get Post Owner Id
    public async Task<string> GetPostOwnerId(string postId)
    {
        var post = await context.UserCollection.Find(c => c.JobPost.Contains(postId)).FirstAsync();
        return post.Id.ToString();
    }
    #endregion

    public async Task<IEnumerable<User>> GetAllUser()
    {
        var userService = new UserService(this.settings);
        return await userService.GetAllAsync();
    }
}