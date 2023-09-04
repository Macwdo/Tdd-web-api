using TddWebApi.Models;

namespace TddWebApi.Services;

public class UsersService: IUserService
{
    public Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }
}