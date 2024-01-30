using Microsoft.Extensions.Options;
using Refit;
using TddWebApi.Middlewares;
using TddWebApi.Options;
using TddWebApi.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Middlewares

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

#endregion

#region Services

# region Users
builder.Services.Configure<UsersApiOptions>(builder.Configuration.GetSection("UsersApiOptions"));
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services
    .AddRefitClient<IUsersApi>()
    .ConfigureHttpClient((serviceProvider, client) => {
        var baseUrl = serviceProvider.GetRequiredService<IOptions<UsersApiOptions>>().Value.BaseUrl;
        client.BaseAddress = new Uri(baseUrl);
    });
#endregion

#endregion


// void ConfigureServices(IServiceCollection services)
// {
//     services.AddTransient<IUserService, UsersService>();
// }

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

#region Middlewares

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
