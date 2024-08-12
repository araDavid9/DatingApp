using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt =>
{
  
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); // The string inside must be same as the string in the json file 
});

var app = builder.Build();

app.MapControllers();

app.Run();
