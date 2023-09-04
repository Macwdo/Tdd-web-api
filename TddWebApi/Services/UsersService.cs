using System.Net;
using Microsoft.Extensions.Options;
using TddWebApi.Configs;
using TddWebApi.Models;

namespace TddWebApi.Services;

public class UsersService: IUsersService
{
    private readonly HttpClient _httpClient;
    private readonly UsersApiOptions _apiConfigs;

    public UsersService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfigs)
    {
        _httpClient = httpClient;
        _apiConfigs = apiConfigs.Value;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var users = await _httpClient.GetAsync(_apiConfigs.Endpoint);
        if (users.StatusCode == HttpStatusCode.NotFound)
        {
            return new List<User>() { };
        }

        var responseContent = users.Content;
        var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
        return allUsers.ToList();

    }
}