using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CarShowRoom.Models;


namespace CarShowRoom;

public class CarShowRoomContext : DbContext
{
    
    public CarShowRoomContext(DbContextOptions<CarShowRoomContext> options)
            : base(options)
        {
        }
    
    public DbSet<Car> Cars { get; set; }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ServiceHistory> ServiceHistories { get; set; }
    
    
    public DbSet<CarType> CarTypes { get; set; }
    public DbSet<CarBrand> CarBrands { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("Default");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Sale>()
            .HasOne(s => s.Customer)
            .WithMany()
            .HasForeignKey(s => s.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
      
        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", PhoneNumber = "1234567890" },
            new Customer { CustomerId = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@yahoo.com", PhoneNumber = "0987654321" },
            new Customer { CustomerId = 3, FirstName = "Michael", LastName = "Johnson", Email = "michael.johnson@outlook.com", PhoneNumber = "1112223333" },
            new Customer { CustomerId = 4, FirstName = "Emily", LastName = "Brown", Email = "emily.brown@mail.com", PhoneNumber = "4445556666" },
            new Customer { CustomerId = 5, FirstName = "David", LastName = "Williams", Email = "david.williams@gmail.com", PhoneNumber = "7778889999" },
            new Customer { CustomerId = 6, FirstName = "Sophia", LastName = "Miller", Email = "sophia.miller@live.com", PhoneNumber = "5554443333" },
            new Customer { CustomerId = 7, FirstName = "Daniel", LastName = "Anderson", Email = "daniel.anderson@gmail.com", PhoneNumber = "2221110000" },
            new Customer { CustomerId = 8, FirstName = "Olivia", LastName = "Martinez", Email = "olivia.martinez@aol.com", PhoneNumber = "6667778888" },
            new Customer { CustomerId = 9, FirstName = "Matthew", LastName = "Garcia", Email = "matthew.garcia@gmail.com", PhoneNumber = "9998887777" },
            new Customer { CustomerId = 10, FirstName = "Emma", LastName = "Lopez", Email = "emma.lopez@yahoo.com", PhoneNumber = "3332221111" }
        );
       
        
        modelBuilder.Entity<CarBrand>().HasData(
            new CarBrand { CarBrandId = 1, BrandName = "Toyota" },
            new CarBrand { CarBrandId = 2, BrandName = "BMW" },
            new CarBrand { CarBrandId = 3, BrandName = "Mercedes" }
        );

        modelBuilder.Entity<CarType>().HasData(
            new CarType { CarTypeId = 1, TypeName = "Sedan" },
            new CarType { CarTypeId = 2, TypeName = "SUV" }
        );
        
        
        modelBuilder.Entity<Car>().HasData(
            new Car { CarId = 1, Brand = "Toyota", Model = "Camry", Year = 2021, Price = 30000, CarBrandId = 1, CarTypeId = 1 },
            new Car { CarId = 2, Brand = "BMW", Model = "X5", Year = 2022, Price = 60000, CarBrandId = 2, CarTypeId = 2 },
            new Car { CarId = 3, Brand = "Mercedes", Model = "C-Class", Year = 2020, Price = 45000, CarBrandId = 3, CarTypeId = 1 },
            new Car { CarId = 4, Brand = "Toyota", Model = "RAV4", Year = 2023, Price = 35000, CarBrandId = 1, CarTypeId = 2 },
            new Car { CarId = 5, Brand = "BMW", Model = "3 Series", Year = 2019, Price = 40000, CarBrandId = 2, CarTypeId = 1 } 
        );
        

        modelBuilder.Entity<Employee>().HasData(
            new Employee { EmployeeId = 1, FirstName = "Robert", LastName = "Taylor", Salary = 5000 },
            new Employee { EmployeeId = 2, FirstName = "Linda", LastName = "Harris", Salary = 5500 },
            new Employee { EmployeeId = 3, FirstName = "William", LastName = "Clark", Salary = 6000 }
        );
        
        modelBuilder.Entity<Sale>().HasData(
            new Sale { SaleId = 1, CarId = 1, CustomerId = 1, EmployeeId = 1, SaleDate = new DateTime(2024, 1, 15), SalePrice = 29000 },
            new Sale { SaleId = 2, CarId = 2, CustomerId = 2, EmployeeId = 2, SaleDate = new DateTime(2024, 2, 1), SalePrice = 58000 },
            new Sale { SaleId = 3, CarId = 3, CustomerId = 3, EmployeeId = 3, SaleDate = new DateTime(2024, 3, 10), SalePrice = 43000 },
            new Sale { SaleId = 4, CarId = 4, CustomerId = 4, EmployeeId = 1, SaleDate = new DateTime(2024, 3, 25), SalePrice = 34000 },
            new Sale { SaleId = 5, CarId = 5, CustomerId = 5, EmployeeId = 2, SaleDate = new DateTime(2024, 4, 5), SalePrice = 39000 }
        );
        
        modelBuilder.Entity<ServiceHistory>().HasData(
            new ServiceHistory { ServiceHistoryId = 1, CarId = 1, ServiceDate = new DateTime(2023, 6, 10), ServiceDescription = "Routine maintenance, oil change" },
            new ServiceHistory { ServiceHistoryId = 2, CarId = 2, ServiceDate = new DateTime(2023, 7, 20), ServiceDescription = "Brake pad replacement" },
            new ServiceHistory { ServiceHistoryId = 3, CarId = 3, ServiceDate = new DateTime(2023, 8, 5), ServiceDescription = "Engine diagnostics" },
            new ServiceHistory { ServiceHistoryId = 4, CarId = 4, ServiceDate = new DateTime(2023, 9, 15), ServiceDescription = "Wheel balancing" },
            new ServiceHistory { ServiceHistoryId = 5, CarId = 5, ServiceDate = new DateTime(2023, 10, 30), ServiceDescription = "Spark plug replacement" }
        );
        
        
        


    }
    
}