using BlazorsToDoAPI.DbContexts;
using BlazorsToDoAPI.Models;
using MongoDB.Driver;

namespace BlazorsToDoAPI.Repositories
{
    public class MongoDbTaskRepository : IRepository<TaskModel>
    {
        public MongoDbTaskRepository(IMongoDbContext<TaskModel> context)
        {
            _context = context;
            _mongoCollection = _context.GetMongoDbCollection("test-todos");
        }

        private readonly IMongoDbContext<TaskModel> _context;
        protected IMongoCollection<TaskModel> _mongoCollection;
        public FilterDefinitionBuilder<TaskModel> _filterDefinition = Builders<TaskModel>.Filter;
        public async Task<IEnumerable<TaskModel>> GetAllAsync() =>
            await _mongoCollection.AsQueryable().ToListAsync();
        public async Task<TaskModel> GetById(Guid guid)
        {
            var filter = _filterDefinition.Eq(item => item.Id, guid);
            return await _mongoCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task Create(TaskModel item) =>
            await _mongoCollection.InsertOneAsync(item);

        public async Task Update(TaskModel item)
        {
            var updatedFilter = _filterDefinition.Eq(x => x.Id, item.Id);
            await _mongoCollection.ReplaceOneAsync(updatedFilter, item);
        }
        public async Task Delete(Guid guid)
        {
            var deleteFilter = _filterDefinition.Eq(item => item.Id, guid);
            await _mongoCollection.DeleteOneAsync(deleteFilter);
        }

    }
}
 