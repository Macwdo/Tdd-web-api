using Refit;
using TddWebApi.Models;

namespace TddWebApi.Services.Users;
public interface IUsersApi {

    [Get(Endpoints.Users)]
    Task<IEnumerable<User>> GetAllUsers();

    [Get(Endpoints.Users + "/{id}")]
    Task<User?> GetUser(int id);
}