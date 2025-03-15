using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EfCore.Models;

public class CarShowRoomContextFactory : IDesignTimeDbContextFactory<CarShowRoomContext>
{
    public CarShowRoomContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CarShowRoomContext>();
        optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=EfCoreDB; User Id=sa; Password=admin; Trust Server Certificate=true;");

        return new CarShowRoomContext(optionsBuilder.Options);
    }
}