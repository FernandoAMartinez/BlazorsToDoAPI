using BlazorsToDoAPI.Models;
using MongoDB.Driver;

namespace BlazorsToDoAPI.Repositories
{
    public class TaskRepository : BaseRepository<TaskModel>, ITaskRepository
    {
        public TaskRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle)
            : base(mongoClient, clientSessionHandle, "TASK_COLLECTION")
        {

        }
        public async Task<IEnumerable<TaskModel>> GetAll() =>
            await Collection.AsQueryable().ToListAsync();

        public async Task<TaskModel> GetById(Guid guid)
        {
            var filter = Builders<TaskModel>.Filter.Eq(s => s.Id, guid);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TaskModel>> GetByUserId(Guid userGuid)
        {
            var filter = Builders<TaskModel>.Filter.Eq(s => s.UserId, userGuid);
            return await Collection.Find(filter).ToListAsync();
        }
    }
}
