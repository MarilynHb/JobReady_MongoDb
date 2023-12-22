using JobReady.Interfaces;
using JobReady.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace JobReady.Services;

public class UserService : IUserService
{
    private readonly MongoDbContext context;
    public UserService(IOptions<MongoDbConfiguration> settings)
    {
        context = new MongoDbContext(settings);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.UserCollection.Find(c => true).ToListAsync();
    }
    public async Task<User> GetByIdAsync(string id)
    {
        return await context.UserCollection.Find(c => c.Id.ToString() == id).FirstOrDefaultAsync();
    }
    public async Task<User> CreateAsync(User user)
    {
        user.CreatedOn = DateTime.Now;
        user.ModifiedOn = DateTime.Now;
        await context.UserCollection.InsertOneAsync(user);
        return user;
    }
    public async Task UpdateAsync(string id, User user)
    {
        user.ModifiedOn = DateTime.Now;
        await context.UserCollection.ReplaceOneAsync(c => c.Id.ToString() == id, user);
    }
    public async Task DeleteAsync(string id)
    {
        await context.UserCollection.DeleteOneAsync(c => c.Id.ToString() == id);
    }
}
