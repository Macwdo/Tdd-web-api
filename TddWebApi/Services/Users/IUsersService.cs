using TddWebApi.Models;

namespace TddWebApi.Services.Users;
public interface IUsersService
{
    Task<IEnumerable<User>> GetUsers();
    Task<User?> GetUser(int id);
}