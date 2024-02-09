using TddWebApi.Models;

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

    public async Task<IEnumerable<User>> GetUsers()
    {
        try 
        {
            var users = await _usersApi.GetAllUsers();
            return users;
        } 
        catch (Exception) 
        {
            _logger.LogError("Error trying to get users.");
            throw;
        }
        
    }

    public async Task<User?> GetUser(int id)
    {
        try 
        {
            var user = await _usersApi.GetUser(id);
            if (user != null) return user;
            _logger.LogWarning($"User by id {id} not found");
            return null;
        } 
        catch (Exception) 
        {
            _logger.LogError($"Error trying to get user by id {id}");
            throw;
        }
    }
}