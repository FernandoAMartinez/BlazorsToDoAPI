using BlazorsToDoAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace BlazorsToDoAPI.Repositories;
public class LoginRepository : BaseRepository<User>, ILoginRepository
{
    public LoginRepository(IMongoClient mongoClient, IClientSessionHandle clientSessionHandle) :
        base(mongoClient, clientSessionHandle, "USERS_COLLECTION")
    {
    }

    public async Task<User> GetLoginFromCredentialsAsync(LoginRequest request)
    {
        var filter = Builders<User>.Filter.Eq(s => s.Email, request.Email) &
            Builders<User>.Filter.Eq(s => s.Password, request.Password);

        var result = await Collection.Find(filter).FirstOrDefaultAsync();
        return result;
    }

    public async Task<IEnumerable<User>> GetAll() =>
        await Collection.AsQueryable().ToListAsync();
}