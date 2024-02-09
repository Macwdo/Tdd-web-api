using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using TddWebApi.Services.Users;
using Xunit;

namespace TddWebApi.Tests.Fixtures;

public class WebApplicationFixture: IAsyncLifetime
{
    public HttpClient Client { get; set; }
    private readonly WebApplicationFactory<Program> _factory;

    public WebApplicationFixture()
    {
        _factory = new WebApplicationFactory<Program>();
        _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IUsersService, UsersService>();
                // You can configure your DI here
                // For example you can remove your DbContextOptions and add a new one

                // services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                // services.AddDbContext<ApplicationDbContext>(options =>
                // {
                //     options.UseInMemoryDatabase("test");
                // });
            });
        });
        Client = _factory.CreateClient();
    }


    /// <summary>
    /// This method is called before each test
    /// </summary>
    /// <returns></returns>
    public Task InitializeAsync()
    {
        // You can add some initialization code here
        // For example initialize your database

        using var scope = _factory.Services.CreateScope();
        {
            var services = scope.ServiceProvider;
            // var context = services.GetRequiredService<ApplicationDbContext>();
            // await context.Database.EnsureCreatedAsync();
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// This method is called after each test
    /// </summary>
    /// <returns></returns>
    public Task DisposeAsync()
    {
        // You can add some cleanup code here
        // For example delete your database

        using var scope = _factory.Services.CreateScope();
        {
            var services = scope.ServiceProvider;
            // var context = services.GetRequiredService<ApplicationDbContext>();
            // await context.Database.EnsureDeletedAsync();
        }
        return Task.CompletedTask;
    }
}