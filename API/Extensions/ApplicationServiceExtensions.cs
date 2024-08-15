using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Services;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices
        (this IServiceCollection  i_Services,IConfiguration i_Config)
    {
        i_Services.AddControllers();
        i_Services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(i_Config.GetConnectionString("DefaultConnection")); // The string inside must be same as the string in the json file 
        });
        i_Services.AddCors();
        i_Services.AddScoped<ITokenService, TokenService>();
        
    }
    
    
    
}