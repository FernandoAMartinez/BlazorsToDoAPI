using BlazorsToDoAPI.Models;

namespace BlazorsToDoAPI.Repositories;
public interface ITaskRepository : IRepositoryBase<TaskResponse>
{
    Task<TaskResponse> GetById(Guid guid);
    Task<IEnumerable<TaskResponse>> GetByUserId(Guid userGuid);
    Task<IEnumerable<TaskResponse>> GetAll();
}