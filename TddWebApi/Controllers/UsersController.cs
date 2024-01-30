using Microsoft.AspNetCore.Mvc;
using TddWebApi.Models;
using TddWebApi.Services.Users;

namespace TddWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet(Name = "GetUsers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Get()
    {
        var users = await _usersService.GetAllUsers();
        if (!users.Any()) return NotFound();
        return Ok(users);
    }

    [HttpGet("{id}", Name = "GetUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _usersService.GetUser(id);
        if (user is null) return NotFound();
        return Ok(user);
    }
}
