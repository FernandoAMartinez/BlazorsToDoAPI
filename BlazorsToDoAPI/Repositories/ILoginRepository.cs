using BlazorsToDoAPI.Models;

namespace BlazorsToDoAPI.Repositories
{
    internal interface ILoginRepository : IRepositoryBase<User>
    {
        Task<User> GetLoginFromCredentialsAsync(LoginRequest request);
        Task<IEnumerable<User>> GetAll();

    }
}
