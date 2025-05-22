using Test1.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<IUsers, Users>();

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
