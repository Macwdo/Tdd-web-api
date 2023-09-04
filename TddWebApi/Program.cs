using TddWebApi.Configs;
using TddWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<UsersApiOptions>(builder.Configuration.GetSection("UsersApiOptions"));
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddHttpClient<IUsersService, UsersService>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
