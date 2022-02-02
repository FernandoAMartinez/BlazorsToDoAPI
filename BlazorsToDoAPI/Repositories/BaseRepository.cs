using MongoDB.Driver;
using System.Linq.Expressions;

namespace BlazorsToDoAPI.Repositories
{
    public class BaseRepository<T> : IRepositoryBase<T> where T : class
    {
        private readonly IMongoClient _mongoClient;
        private readonly IClientSessionHandle _clientSessionHandle;
        private readonly FilterDefinitionBuilder<T> _filterDefinition = Builders<T>.Filter;
        private readonly string _collection;

        public BaseRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle, string collection)
        {
            (_mongoClient, _clientSessionHandle, _collection) = (mongoClient, clientSessionHandle, Environment.GetEnvironmentVariable(collection));

            if (!_mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_BASENAME")).
                ListCollectionNames().ToList().Contains(_collection))
            {
                _mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_BASENAME")).CreateCollection(_collection);
            }
        }

        // Main property with the collection of objects
        protected virtual IMongoCollection<T> Collection =>
            _mongoClient.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_BASENAME")).GetCollection<T>(_collection);
        public async Task Create(T obj) 
        {
            _clientSessionHandle.StartTransaction();
            try
            {
                await Collection.InsertOneAsync(_clientSessionHandle, obj);
                await _clientSessionHandle.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _clientSessionHandle.AbortTransactionAsync();
            }
        }
        public async Task Update(T obj)
        {
            _clientSessionHandle.StartTransaction();
            try
            {
                var filter = _filterDefinition.Eq("_id", obj.GetType().GetProperty("Id").GetValue(obj, null));
                if (obj is not null)
                {
                    await Collection.ReplaceOneAsync(_clientSessionHandle, filter, obj);
                    await _clientSessionHandle.CommitTransactionAsync();
                }
            }
            catch (Exception)
            {
                await _clientSessionHandle.AbortTransactionAsync();
            }
        }
        public async Task Delete(Guid guid)
        {
            _clientSessionHandle.StartTransaction();
            
            try
            {
                var filter = _filterDefinition.Eq("id", guid);
                await Collection.DeleteOneAsync(_clientSessionHandle, filter);
                await _clientSessionHandle.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _clientSessionHandle.AbortTransactionAsync();
            }
        }
    }
}
