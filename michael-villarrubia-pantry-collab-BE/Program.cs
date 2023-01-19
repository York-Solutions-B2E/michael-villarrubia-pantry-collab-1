global using Microsoft.EntityFrameworkCore;
global using michael_villarrubia_pantry_collab_BE.Models;
using michael_villarrubia_pantry_collab_BE.Services.UserService;
using michael_villarrubia_pantry_collab_BE.Services.FamilyService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<michael_villarrubia_pantry_collab_BE.DataContext>(options =>
{
    options.UseSqlServer("Server=localhost;Initial Catalog=StoreDB;Integrated Security=False;User Id=sa;Password=Your_password123;MultipleActiveResultSets=True");
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFamilyService, FamilyService>();

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
