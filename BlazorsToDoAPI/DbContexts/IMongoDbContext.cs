using MongoDB.Driver;

namespace BlazorsToDoAPI.DbContexts
{
    public interface IMongoDbContext<T> where T : class
    {
        IMongoCollection<T> GetMongoDbCollection(string collectionName);
    }
}
