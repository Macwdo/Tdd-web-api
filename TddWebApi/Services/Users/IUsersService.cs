using TddWebApi.Models;

namespace TddWebApi.Services.Users;
public interface IUsersService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUser(int id);
}