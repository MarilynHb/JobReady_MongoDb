using JobReady.Interfaces;
using JobReady.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace JobReady.Services;

public class UniversityService : IUniversityService
{
    private readonly MongoDbContext context;
    public UniversityService(IOptions<MongoDbConfiguration> settings)
    {
        context = new MongoDbContext(settings);
    }

    public async Task<IEnumerable<University>> GetAllAsync()
    {
        return await context.UniversityCollection.Find(c => true).ToListAsync();
    }
    public async Task<University> GetByIdAsync(string id)
    {
        return await context.UniversityCollection.Find(c => c.Id.ToString() == id).FirstOrDefaultAsync();
    }
    public async Task<University> CreateAsync(University university)
    {
        await context.UniversityCollection.InsertOneAsync(university);
        return university;
    }
    public async Task UpdateAsync(string id, University university)
    {
        await context.UniversityCollection.ReplaceOneAsync(c => c.Id.ToString() == id, university);
    }
    public async Task DeleteAsync(string id)
    {
        await context.UniversityCollection.DeleteOneAsync(c => c.Id.ToString() == id);
    }
}
