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

    #region Get User
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.UserCollection.Find(c => true).ToListAsync();
    }

    public async Task<User> GetByIdAsync(string id)
    {
        return await context.UserCollection.Find(c => c.Id.ToString() == id).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<University>> GetAllUniversitiesAsync()
    {
        return await context.UniversityCollection.Find(c => true).ToListAsync();
    }
    public async Task<IEnumerable<EducationByUser>> GetEducationByUsers()
    {
        var users = await GetAllAsync();
        var universities = await GetAllUniversitiesAsync();
        List<EducationByUser> result = [];
        foreach (var user in users)
        {
            var i = 0;
            var educations = from y in user.Education
                             join u in universities on y.UniversityId equals u.Id.ToString() into g
                             select new EducationByUser()
                             {
                                 Index = i++,
                                 OwnerId = user.Id.ToString(),
                                 FullName = user.FirstName + " " + user.LastName,
                                 University = g.First().Name,
                                 Major = y.Major,
                                 Degree = y.Degree,
                                 StartDate = y.StartDate,
                                 EndDate = y.EndDate
                             };
            result.AddRange(educations);
        }
        return result.AsEnumerable();
    }
    #endregion

    #region Create User
    public async Task<User> CreateAsync(User user)
    {
        user.CreatedOn = DateTime.Now;
        user.ModifiedOn = DateTime.Now;
        await context.UserCollection.InsertOneAsync(user);
        return user;
    }
    #endregion

    #region Update User
    public async Task UpdateAsync(string id, User user)
    {
        user.ModifiedOn = DateTime.Now;
        await context.UserCollection.ReplaceOneAsync(c => c.Id.ToString() == id, user);
    }
    #endregion

    #region Delete User
    public async Task DeleteAsync(string id)
    {
        await context.UserCollection.DeleteOneAsync(c => c.Id.ToString() == id);
    }
    #endregion

    #region Add Education
    public async Task AddEducation(AddEducationRequest request)
    {
        var user = await GetByIdAsync(request.OwnerId);
        var education = new Education
        {
            UniversityId = request.UniversityId,
            Major = request.Major,
            Degree = request.Degree,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
        user.Education = user.Education.Concat([education]);
        await UpdateAsync(request.OwnerId, user);
    }
    #endregion

    #region Update Education
    public async Task UpdateEducation(AddEducationRequest request)
    {
        var user = await GetByIdAsync(request.OwnerId);
        user.Education.ElementAt(request.Index).UniversityId = request.UniversityId;
        user.Education.ElementAt(request.Index).Major = request.Major;
        user.Education.ElementAt(request.Index).Degree = request.Degree;
        user.Education.ElementAt(request.Index).StartDate = request.StartDate;
        user.Education.ElementAt(request.Index).EndDate = request.EndDate;
        await UpdateAsync(request.OwnerId, user);
    }
    #endregion
}
