using BlazorsToDoAPI.Models;

namespace BlazorsToDoAPI.Repositories
{
    public interface ICommentRepository : IRepositoryBase<CommentResponse>
    {
        Task<IEnumerable<CommentResponse>> GetAll();
        Task<IEnumerable<CommentResponse>> GetByTaskId(Guid taskGuid);
        Task<CommentResponse> GetById(Guid guid);
    }
}
