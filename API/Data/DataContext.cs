using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    public DbSet<AppUser> Users { get; set; }
    
    public DataContext(DbContextOptions i_Options) : base(i_Options)
    {
       
    }
}