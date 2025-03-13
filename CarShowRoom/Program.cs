using System;
using CarShowRoom;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main()
    {
        var options = new DbContextOptionsBuilder<CarShowRoomContext>()
            .UseSqlServer("Data Source=localhost; Initial Catalog=CarShowRoomDB; User Id=sa; Password=admin; Trust Server Certificate=true;")  
            .Options;

        using (var context = new CarShowRoomContext(options))  
        {
            CrudOperations service = new CrudOperations(context);
            service.RunMenu();
        }
    }
}