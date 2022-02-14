using BlazorsToDoAPI.Models;
using MongoDB.Driver;

namespace BlazorsToDoAPI.Repositories;
public class TaskRepository : BaseRepository<TaskResponse>, ITaskRepository
{
    public TaskRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle)
        : base(mongoClient, clientSessionHandle, "TASK_COLLECTION")
    {

    }
    public async Task<IEnumerable<TaskResponse>> GetAll() =>
        await Collection.AsQueryable().ToListAsync();

    public async Task<TaskResponse> GetById(Guid guid)
    {
        var filter = Builders<TaskResponse>.Filter.Eq(s => s.Id, guid);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TaskResponse>> GetByUserId(Guid userGuid)
    {
        var filter = Builders<TaskResponse>.Filter.Eq(s => s.UserId, userGuid);
        return await Collection.Find(filter).ToListAsync();
    }

    //public async Task UpdateTaskAsync(TaskRequest request)
    //{
    //    var filter = Builders<TaskResponse>.Filter.Eq(s => s.Id, request.Id);
    //    var updatedTask = await Collection.FindAsync(filter);
    //    if (updatedTask != null)
    //    {
            
    //    }
    //}
}
