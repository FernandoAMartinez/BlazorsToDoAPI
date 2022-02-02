using MongoDB.Driver;

namespace BlazorsToDoAPI.DbContexts
{
    public class MongoDbContext<T> : IMongoDbContext<T> where T : class
    {

        public MongoDbContext(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, string collection)
        {
            //(_mongoClient, _clientSessionHandle, _collection) = (mongoClient, clientSessionHandle, collection);
            (_mongoClient, _clientSessionHandle) = (mongoClient, clientSessionHandle);

            if (!_mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_BASENAME")).
                ListCollectionNames().ToList().Contains(collection))
            {
                _mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_BASENAME")).CreateCollection(collection);
            }
        }

        //private readonly string _collection;
        private readonly IMongoClient _mongoClient;
        public IClientSessionHandle _clientSessionHandle;

        //protected virtual IMongoCollection<T> Collection =>
        //    _mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_BASENAME")).GetCollection<T>(_collection);

        public IMongoCollection<T> GetMongoDbCollection(string collectionName) =>
            _mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_BASENAME")).GetCollection<T>(collectionName);
    }
}
