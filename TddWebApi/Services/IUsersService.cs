using TddWebApi.Models;

namespace TddWebApi.Services;

public interface IUsersService
{
    Task<IEnumerable<User>> GetAllUsers();
}