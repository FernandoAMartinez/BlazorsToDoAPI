namespace BlazorsToDoAPI.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task Create(T obj);
        Task Update(T obj);
        Task Delete(Guid guid);
    }
}