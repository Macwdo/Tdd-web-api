using TddWebApi.Models;
using TddWebApi.Services.Users.Exceptions;

namespace TddWebApi.Services.Users;
public class UsersService: IUsersService
{
    private readonly ILogger<UsersService> _logger;
    private readonly IUsersApi _usersApi;

    public UsersService(IUsersApi usersApi, ILogger<UsersService> logger)
    {
        _usersApi = usersApi;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        try 
        {
            var users = await _usersApi.GetUsers();
            return users;
        } 
        catch (Exception) 
        {
            const string ErrorMessage = "Error trying to get users.";
            _logger.LogError(ErrorMessage);
            throw new GetUsersException(ErrorMessage);
        }
        
    }

    public async Task<User?> GetUser(int id)
    {
        try 
        {
            var user = await _usersApi.GetUser(id);
            if (user == null) {
                _logger.LogWarning($"User by id {id} not found");
                return null;
            }
            return user;
        } 
        catch (Exception) 
        {
            var errorMessage = $"Error trying to get user by id {id}";
            _logger.LogError(errorMessage);
            throw new GetUsersException(errorMessage);
        }
    }
}