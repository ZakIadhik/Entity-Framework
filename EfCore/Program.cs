using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using CarShowRoom.Services;
using EfCore.Models;
using EfCore.Services;

namespace CarShowRoom
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddDbContext<CarShowRoomContext>(options =>
                    options.UseSqlServer("Data Source=localhost; Initial Catalog=EfCoreDB; User Id=sa; Password=admin; Trust Server Certificate=true;")
                        .UseLazyLoadingProxies()
                        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information),
                ServiceLifetime.Scoped); 

            services.AddSingleton<CarService>();
            services.AddSingleton<CustomerService>();
            services.AddSingleton<DealerService>();
            services.AddSingleton<OrderService>();

            var serviceProvider = services.BuildServiceProvider();
            RunApplication(serviceProvider);
        }

        static void RunApplication(ServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var scopedProvider = scope.ServiceProvider;

            var dbContext = scopedProvider.GetRequiredService<CarShowRoomContext>();
            dbContext.Database.EnsureCreated(); 

            var carService = scopedProvider.GetRequiredService<CarService>();
            var customerService = scopedProvider.GetRequiredService<CustomerService>();
            var saleService = scopedProvider.GetRequiredService<OrderService>();
            var dealerService = scopedProvider.GetRequiredService<DealerService>();

            QueryTester.TestQueries(scopedProvider);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nSelect an action:");
                Console.WriteLine("1. Add a new dealer");
                Console.WriteLine("2. Add a new car");
                Console.WriteLine("3. View all cars");
                Console.WriteLine("4. Add a new customer");
                Console.WriteLine("5. View all customers");
                Console.WriteLine("6. Make a sale");
                Console.WriteLine("7. View all sales");
                Console.WriteLine("8. Update a car");
                Console.WriteLine("9. Delete a car");
                Console.WriteLine("10. Exit");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter dealer name:");
                        string dealerName = Console.ReadLine();
                        Console.WriteLine("Enter dealer location:");
                        string dealerLocation = Console.ReadLine();
                        var dealerId = dealerService.AddDealer(dealerName, dealerLocation);
                        Console.WriteLine($"Dealer added with ID: {dealerId}");
                        break;

                    case "2":
                        Console.WriteLine("Enter car brand:");
                        string brand = Console.ReadLine();
                        Console.WriteLine("Enter car model:");
                        string model = Console.ReadLine();
                        Console.WriteLine("Enter car year:");
                        if (!int.TryParse(Console.ReadLine(), out int year))
                        {
                            Console.WriteLine("Error: Invalid year input.");
                            break;
                        }

                        Console.WriteLine("Enter dealer ID:");
                        dealerService.ListDealers();
                        if (!int.TryParse(Console.ReadLine(), out int dealerIdInput))
                        {
                            Console.WriteLine("Error: Invalid dealer ID input.");
                            break;
                        }

                        carService.AddCar(brand, model, year, dealerIdInput);
                        break;

                    case "3":
                        Console.WriteLine("List of all cars:");
                        carService.GetAllCars();
                        break;

                    case "4":
                        Console.WriteLine("Enter customer first name:");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Enter customer last name:");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Enter customer email:");
                        string email = Console.ReadLine();
                        Console.WriteLine("Enter customer phone number:");
                        string phone = Console.ReadLine();
                        customerService.AddCustomer(firstName, lastName, email, phone);
                        break;

                    case "5":
                        Console.WriteLine("List of all customers:");
                        customerService.GetAllCustomers();
                        break;

                    case "6":
                        Console.WriteLine("Enter car ID:");
                        if (!int.TryParse(Console.ReadLine(), out int carId))
                        {
                            Console.WriteLine("Error: Invalid car ID.");
                            break;
                        }

                        Console.WriteLine("Enter customer ID:");
                        if (!int.TryParse(Console.ReadLine(), out int customerId))
                        {
                            Console.WriteLine("Error: Invalid customer ID.");
                            break;
                        }

                        saleService.MakeSale(carId, customerId);
                        break;

                    case "7":
                        Console.WriteLine("List of all sales:");
                        saleService.GetAllOrders();
                        break;

                    case "8":
                        Console.WriteLine("Enter car ID to update:");
                        if (!int.TryParse(Console.ReadLine(), out int updateCarId))
                        {
                            Console.WriteLine("Error: Invalid car ID.");
                            break;
                        }

                        Console.WriteLine("Enter new brand:");
                        string newMake = Console.ReadLine();
                        Console.WriteLine("Enter new model:");
                        string newModel = Console.ReadLine();
                        Console.WriteLine("Enter new year:");
                        if (!int.TryParse(Console.ReadLine(), out int newYear))
                        {
                            Console.WriteLine("Error: Invalid year input.");
                            break;
                        }

                        carService.UpdateCar(updateCarId, newMake, newModel, newYear);
                        break;

                    case "9":
                        Console.WriteLine("Enter car ID to delete:");
                        if (!int.TryParse(Console.ReadLine(), out int deleteCarId))
                        {
                            Console.WriteLine("Error: Invalid car ID.");
                            break;
                        }

                        carService.DeleteCar(deleteCarId);
                        break;

                    case "10":
                        Console.WriteLine("Exiting program...");
                        return;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}