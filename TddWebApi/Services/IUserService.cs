using TddWebApi.Models;

namespace TddWebApi.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
}