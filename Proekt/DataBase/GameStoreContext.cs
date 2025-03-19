using Microsoft.EntityFrameworkCore;
using Proekt.Models;


namespace Proekt.DataBase;

public class GameStoreContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderStruct> OrderStructs { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    
    
    public GameStoreContext()
    {
    }

    public GameStoreContext(DbContextOptions<GameStoreContext> options)
        : base(options)
    {
    }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=GameStoreDB; User Id=sa; Password=admin; Trust Server Certificate=true;");
        }
    }
}
