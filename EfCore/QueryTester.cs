using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using EfCore.Models;

namespace CarShowRoom
{
    public static class QueryTester
    {
        public static void TestQueries(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<CarShowRoomContext>();

            Console.WriteLine("\n✅ Testing Eager Loading: Dealers and their cars");
            var dealers = context.Dealers.Include(d => d.Cars).ToList();
            foreach (var dealer in dealers)
            {
                Console.WriteLine($"Dealer: {dealer.Name}");
                foreach (var car in dealer.Cars)
                {
                    Console.WriteLine($" - {car.Make} {car.Model} ({car.Year})");
                }
            }

            Console.WriteLine("\n✅ Testing Explicit Loading: Loading dealer for a single car");
            var carToLoad = context.Cars.FirstOrDefault();
            if (carToLoad != null)
            {
                context.Entry(carToLoad).Reference(c => c.Dealer).Load();
                Console.WriteLine($"Car: {carToLoad.Make} {carToLoad.Model}, Dealer: {carToLoad.Dealer?.Name}");
            }

            Console.WriteLine("\n✅ Testing Lazy Loading: Car -> Dealer");
            var lazyCar = context.Cars.FirstOrDefault();
            if (lazyCar != null)
            {
                Console.WriteLine($"Dealer (Lazy Load): {lazyCar.Dealer?.Name}");
            }

            Console.WriteLine("\n✅ Testing SQL Query (FromSqlRaw): All Toyota cars");
            var toyotaCars = context.Cars.FromSqlRaw("SELECT * FROM Cars WHERE Make = 'Toyota'").ToList();
            foreach (var c in toyotaCars)
            {
                Console.WriteLine($"{c.Make} {c.Model}");
            }
        }
    }

}