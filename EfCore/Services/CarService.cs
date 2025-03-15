using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using EfCore.Models;

namespace CarShowRoom.Services
{
    public class CarService
    {
        private readonly CarShowRoomContext _context;

        public CarService(CarShowRoomContext context)
        {
            _context = context;
        }

        public List<Car> GetCarsWithDetails()
        {
            return _context.Cars
                .Include(c => c.Dealer) 
                .Include(c => c.Orders) 
                .ThenInclude(o => o.Customer) 
                .ToList();
        }

        public void AddCar(string make, string model, int year, int dealerId)
        {
            var dealerExists = _context.Dealers.Any(d => d.Id == dealerId);
            if (!dealerExists)
            {
                Console.WriteLine($"Error: Dealer with ID {dealerId} not found in the database!");
                return;
            }

            var car = new Car { Make = make, Model = model, Year = year, DealerId = dealerId };
            _context.Cars.Add(car);
            _context.SaveChanges();
            Console.WriteLine("Car added!");
        }




        public void UpdateCar(int carId, string newMake, string newModel, int newYear)
        {
            var car = _context.Cars.Find(carId);
            if (car != null)
            {
                car.Make = newMake;
                car.Model = newModel;
                car.Year = newYear;
                _context.SaveChanges();
                Console.WriteLine("Car information has been updated.");
            }
            else
            {
                Console.WriteLine("Car not found.");
            }
        }


        public void DeleteCar(int carId)
        {
            var car = _context.Cars.Find(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
                Console.WriteLine("Car has been deleted.");
            }
            else
            {
                Console.WriteLine("Car not found.");
            }
        }


        public void GetAllCars()
        {
            var cars = _context.Cars.Include(c => c.Dealer).ToList();
            if (cars.Any())
            {
                foreach (var car in cars)
                {
                    Console.WriteLine($"ID: {car.Id}, Make: {car.Make}, Model: {car.Model}, Year: {car.Year}, Dealer: {car.Dealer?.Name ?? "No dealer"}");
                }
            }
            else
            {
                Console.WriteLine("No available cars.");
            }
        }

        
        public void AddCarAndCustomer(string carMake, string carModel, int carYear, int dealerId, string customerFirstName, string customerLastName, string customerEmail, string customerPhoneNumber)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var dealerExists = _context.Dealers.Any(d => d.Id == dealerId);
                if (!dealerExists)
                {
                    Console.WriteLine($"Error: Dealer with ID {dealerId} not found in the database!");
                    return;
                }

                var car = new Car { Make = carMake, Model = carModel, Year = carYear, DealerId = dealerId };
                _context.Cars.Add(car);

                var customer = new Customer 
                { 
                    FirstName = customerFirstName, 
                    LastName = customerLastName, 
                    Email = customerEmail, 
                    PhoneNumber = customerPhoneNumber 
                };
                _context.Customers.Add(customer);

                _context.SaveChanges();

                transaction.Commit();
                Console.WriteLine("Car and customer added successfully.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}