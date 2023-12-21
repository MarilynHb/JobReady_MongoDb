public interface IPostService{
    public Task<List<Post>> GetAllAsync();
}