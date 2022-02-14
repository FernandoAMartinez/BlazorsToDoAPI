using BlazorsToDoAPI.Models;
using MongoDB.Driver;

namespace BlazorsToDoAPI.Repositories;
public class CommentRepository : BaseRepository<CommentResponse>, ICommentRepository
{
    public CommentRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle)
    : base(mongoClient, clientSessionHandle, "COMMENTS_COLLECTION")
    {

    }
    public async Task<IEnumerable<CommentResponse>> GetAll() =>
        await Collection.AsQueryable().ToListAsync();

    public async Task<CommentResponse> GetById(Guid guid)
    {
        var filter = Builders<CommentResponse>.Filter.Eq(s => s.Id, guid);
        return await Collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CommentResponse>> GetByTaskId(Guid taskGuid)
    {
        var filter = Builders<CommentResponse>.Filter.Eq(s => s.TaskId, taskGuid);
        return await Collection.Find(filter).ToListAsync();
    }
}