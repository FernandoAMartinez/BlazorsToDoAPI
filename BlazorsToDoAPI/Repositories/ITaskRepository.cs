using BlazorsToDoAPI.Models;

namespace BlazorsToDoAPI.Repositories
{
    public interface ITaskRepository : IRepositoryBase<TaskModel>
    {
        Task<TaskModel> GetById(Guid guid);
        Task<IEnumerable<TaskModel>> GetByUserId(Guid userGuid);
        Task<IEnumerable<TaskModel>> GetAll();
    }
}
