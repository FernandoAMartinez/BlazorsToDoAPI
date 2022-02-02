namespace BlazorsToDoAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(Guid guid);
        Task Create(T item);
        Task Update(T item);
        Task Delete(Guid guid);
    }
}
