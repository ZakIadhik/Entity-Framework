using System;
using System.Collections.Generic;
using System.Linq;
using CarShowRoom.Models;
using Microsoft.EntityFrameworkCore;



namespace CarShowRoom
{
    public class CrudOperations
    {
        private readonly CarShowRoomContext _context;

        public CrudOperations(CarShowRoomContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public void AddCar()
        {
            try
            {
                Console.Write("Enter brand: ");
                string brand = Console.ReadLine()?.Trim();
                Console.Write("Enter model: ");
                string model = Console.ReadLine()?.Trim();
                Console.Write("Enter year: ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                    throw new Exception("Invalid year.");

                Console.Write("Enter price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
                    throw new Exception("Invalid price. Price must be greater than 0.");

                Console.WriteLine("Select car type:");
                var carTypes = _context.CarTypes.ToList();
                foreach (var carType in carTypes)
                {
                    Console.WriteLine($"{carType.CarTypeId}. {carType.TypeName}");
                }

                if (!int.TryParse(Console.ReadLine(), out int carTypeId) ||
                    !carTypes.Any(ct => ct.CarTypeId == carTypeId))
                    throw new Exception("Invalid car type.");

                Console.WriteLine("Select car brand:");
                var carBrands = _context.CarBrands.ToList();
                foreach (var carBrand in carBrands)
                {
                    Console.WriteLine($"{carBrand.CarBrandId}. {carBrand.BrandName}");
                }

                if (!int.TryParse(Console.ReadLine(), out int carBrandId) ||
                    !carBrands.Any(cb => cb.CarBrandId == carBrandId))
                    throw new Exception("Invalid car brand.");

                var car = new Car
                {
                    Brand = brand,
                    Model = model,
                    Year = year,
                    Price = price,
                    CarTypeId = carTypeId,
                    CarBrandId = carBrandId
                };

                _context.Cars.Add(car);
                _context.SaveChanges();

                Console.WriteLine("Car successfully added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        
        public void DeleteCar()
        {
            try
            {
                Console.Write("Enter the car ID to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new Exception("Invalid ID.");

                var car = _context.Cars.Find(id);
                if (car == null)
                    throw new Exception("Car with this ID not found.");

                _context.Cars.Remove(car);
                _context.SaveChanges(); 

                Console.WriteLine("Car successfully deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }




     
        public void UpdateCar()
        {
            try
            {
                Console.Write("Enter the car ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new Exception("Invalid ID.");

                var car = _context.Cars.Find(id);
                if (car == null)
                    throw new Exception("Car not found.");

                Console.Write("Enter the new price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                    throw new Exception("Invalid price.");

                car.Price = newPrice;

                _context.SaveChanges(); 
                Console.WriteLine("Car successfully updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



    
        public void ShowCarById()
        {
            try
            {
                Console.Write("Enter the car ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new Exception("Invalid ID.");

                var car = _context.Cars.Find(id);
                if (car == null)
                    throw new Exception("Car not found.");

                Console.WriteLine($"ID: {car.CarId}, Brand: {car.Brand}, Model: {car.Model}, Year: {car.Year}, Price: {car.Price:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        
        public void ShowAllCars()
        {
            var cars = _context.Cars.ToList();

            if (cars.Count == 0)
            {
                Console.WriteLine("No cars found.");
                return;
            }

            foreach (var car in cars)
            {
                Console.WriteLine($"ID: {car.CarId}, Brand: {car.Brand}, Model: {car.Model}, Year: {car.Year}, Price: {car.Price:C}");
            }
        }



  
        public IEnumerable<Car> GetAllCars() => _context.Cars.AsNoTracking().ToList();

 
        public IEnumerable<Car> GetCarsByCustomer(int customerId)
        {
            return _context.Sales.Where(c => c.CustomerId == customerId)
                .Include(s => s.Car)
                .Select(s => s.Car)
                .AsNoTracking()
                .ToList();
        }

    
        public IEnumerable<Sale> GetSalesByCar(DateTime startDate, DateTime endDate)
        {
            return _context.Sales.Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .Include(s => s.Car)
                .Include(s => s.Customer)
                .Include(s => s.Employee)
                .AsNoTracking()
                .ToList();
        }

     
        public IEnumerable<object> GetSalesCountEmployee()
        {
            return _context.Sales.GroupBy(s => s.EmployeeId)
                .Select(g => new { EmployeeId = g.Key, SalesCount = g.Count() })
                .ToList();
        }
        
        
        
    
        public void AddCustomer()
        {
            try
            {
                Console.Write("Enter customer's first name: ");
                string firstName = Console.ReadLine()?.Trim();
                Console.Write("Enter customer's last name: ");
                string lastName = Console.ReadLine()?.Trim();
                Console.Write("Enter customer's email: ");
                string email = Console.ReadLine()?.Trim();
                Console.Write("Enter customer's phone number: ");
                string phoneNumber = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(firstName) ||
                    string.IsNullOrWhiteSpace(lastName) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(phoneNumber))
                {
                    throw new Exception("All fields must be filled.");
                }

                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();
                Console.WriteLine("Customer added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



    
        public void DeleteCustomer()
        {
            try
            {
                Console.Write("Enter the customer ID to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new Exception("Invalid ID.");

                var customer = _context.Customers.Find(id);
                if (customer == null)
                    throw new Exception("Customer not found.");

                _context.Customers.Remove(customer);
                _context.SaveChanges();
                Console.WriteLine("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }




        public void UpdateCustomer()
        {
            try
            {
                Console.Write("Enter the customer ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new Exception("Invalid ID.");

                var customer = _context.Customers.Find(id);
                if (customer == null)
                    throw new Exception("Customer not found.");

                Console.Write("Enter the new first name: ");
                customer.FirstName = Console.ReadLine();
                Console.Write("Enter the new last name: ");
                customer.LastName = Console.ReadLine();
                Console.Write("Enter the new email: ");
                customer.Email = Console.ReadLine();
                Console.Write("Enter the new phone number: ");
                customer.PhoneNumber = Console.ReadLine();

                _context.Customers.Update(customer);
                _context.SaveChanges();
                Console.WriteLine("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



  
        public void ShowCustomerById()
        {
            try
            {
                Console.Write("Enter the customer ID to search: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new Exception("Invalid ID.");

                var customer = _context.Customers.Find(id);
                if (customer == null)
                    throw new Exception("Customer not found.");

                Console.WriteLine($"ID: {customer.CustomerId}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Phone: {customer.PhoneNumber}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        
        public void ShowAllCustomers()
        {
            try
            {
                var customers = _context.Customers.AsNoTracking().ToList();
                if (customers.Count == 0)
                {
                    Console.WriteLine("The customer list is empty.");
                    return;
                }

                foreach (var customer in customers)
                {
                    Console.WriteLine($"ID: {customer.CustomerId}, Name: {customer.FirstName} {customer.LastName}, Email: {customer.Email}, Phone: {customer.PhoneNumber}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



 
        public IEnumerable<Customer> GetAllCustomers() => _context.Customers.AsNoTracking().ToList();

    
        public void AddSale()
        {
            try
            {
                Console.Write("Enter car ID: ");
                if (!int.TryParse(Console.ReadLine(), out int carId)) throw new Exception("Invalid car ID.");

                Console.Write("Enter customer ID: ");
                if (!int.TryParse(Console.ReadLine(), out int customerId)) throw new Exception("Invalid customer ID.");

                Console.Write("Enter employee ID: ");
                if (!int.TryParse(Console.ReadLine(), out int employeeId)) throw new Exception("Invalid employee ID.");

                Console.Write("Enter sale price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal salePrice)) throw new Exception("Invalid sale price.");

                var sale = new Sale
                {
                    CarId = carId,
                    CustomerId = customerId,
                    EmployeeId = employeeId,
                    SaleDate = DateTime.Now,
                    SalePrice = salePrice
                };

                _context.Sales.Add(sale);
                _context.SaveChanges();
                Console.WriteLine("Sale added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



  
        public void DeleteSale()
        {
            try
            {
                Console.Write("Enter sale ID to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int id)) throw new Exception("Invalid ID.");

                var sale = _context.Sales.Find(id);
                if (sale == null) throw new Exception("Sale not found.");

                _context.Sales.Remove(sale);
                _context.SaveChanges();
                Console.WriteLine("Sale deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



 
        public void UpdateSale()
        {
            try
            {
                Console.Write("Enter sale ID to update: ");
                if (!int.TryParse(Console.ReadLine(), out int id)) throw new Exception("Invalid ID.");

                var sale = _context.Sales.FirstOrDefault(s => s.SaleId == id);
                if (sale == null) throw new Exception("Sale not found.");

                Console.Write("Enter new sale price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal salePrice)) throw new Exception("Invalid price.");

                sale.SalePrice = salePrice;
                _context.Sales.Update(sale);
                _context.SaveChanges();

                Console.WriteLine("Sale updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }




    
        public void ShowSaleById()
        {
            try
            {
                Console.Write("Enter sale ID to search: ");
                if (!int.TryParse(Console.ReadLine(), out int id)) throw new Exception("Invalid ID.");

                var sale = _context.Sales.FirstOrDefault(s => s.SaleId == id);
                if (sale == null) throw new Exception("Sale not found.");

                Console.WriteLine($"Sale ID: {sale.SaleId}");
                Console.WriteLine($"Car ID: {sale.CarId}");
                Console.WriteLine($"Customer ID: {sale.CustomerId}");
                Console.WriteLine($"Employee ID: {sale.EmployeeId}");
                Console.WriteLine($"Sale Date: {sale.SaleDate:dd.MM.yyyy HH:mm}");
                Console.WriteLine($"Sale Price: {sale.SalePrice:C}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        
        public void ShowAllSales()
        {
            try
            {
                var sales = _context.Sales.AsNoTracking().ToList();
                if (sales.Count == 0)
                {
                    Console.WriteLine("No sales available.");
                    return;
                }

                Console.WriteLine("Sales list:");
                foreach (var sale in sales)
                {
                    Console.WriteLine($"Sale ID: {sale.SaleId}, Car ID: {sale.CarId}, Customer ID: {sale.CustomerId}, Sale Date: {sale.SaleDate:dd.MM.yyyy HH:mm}, Sale Price: {sale.SalePrice:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



    
        public IEnumerable<Sale> GetAllSales() => _context.Sales.AsNoTracking().ToList();

  
        public void AddEmployee()
        {
            try
            {
                Console.Write("Enter employee's first name: ");
                string firstName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(firstName)) throw new Exception("First name cannot be empty.");

                Console.Write("Enter employee's last name: ");
                string lastName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(lastName)) throw new Exception("Last name cannot be empty.");

                var employee = new Employee { FirstName = firstName, LastName = lastName };
                _context.Employees.Add(employee);
                _context.SaveChanges();

                Console.WriteLine("Employee successfully added.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }




      
        public void DeleteEmployee()
        {
            try
            {
                Console.Write("Enter the employee ID to delete: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Error: Invalid ID.");
                    return;
                }

                var employee = _context.Employees.Find(id);
                if (employee == null)
                {
                    Console.WriteLine("Error: Employee not found.");
                    return;
                }

                _context.Employees.Remove(employee);
                _context.SaveChanges();

                Console.WriteLine("Employee successfully deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



     
        public void UpdateEmployee()
        {
            Console.Write("Enter the employee ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Error: Invalid ID.");
                return;
            }

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                Console.WriteLine("Error: Employee not found.");
                return;
            }

            Console.Write("Enter the new first name: ");
            employee.FirstName = Console.ReadLine();

            Console.Write("Enter the new last name: ");
            employee.LastName = Console.ReadLine();

            Console.Write("Enter the new salary: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
            {
                Console.WriteLine("Error: Invalid salary format.");
                return;
            }

            employee.Salary = salary;

            _context.Employees.Update(employee);
            _context.SaveChanges();

            Console.WriteLine("Employee successfully updated.");
        }



      
        public void ShowEmployeeById()
        {
            Console.Write("Enter the employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Error: Invalid ID.");
                return;
            }

            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            Console.WriteLine($"ID: {employee.EmployeeId}, Name: {employee.FirstName} {employee.LastName}, Salary: {employee.Salary}");
        }


        
        public void ShowAllEmployees()
        {
            var employees = _context.Employees.AsNoTracking().ToList();

            if (employees.Count == 0)
            {
                Console.WriteLine("No registered employees.");
                return;
            }

            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.EmployeeId}, Name: {employee.FirstName} {employee.LastName}, Salary: {employee.Salary}");
            }
        }


        public void RunMenu()
        {
            while (true)
            {
                Console.WriteLine("\nSelect an action:");
                Console.WriteLine("1 - Add an employee");
                Console.WriteLine("2 - Delete an employee");
                Console.WriteLine("3 - Update an employee");
                Console.WriteLine("4 - Show all employees");
                Console.WriteLine("5 - Show an employee by ID");
                Console.WriteLine("6 - Add a sale");
                Console.WriteLine("7 - Delete a sale");
                Console.WriteLine("8 - Update a sale");
                Console.WriteLine("9 - Show all sales");
                Console.WriteLine("10 - Show a sale by ID");
                Console.WriteLine("11 - Add a customer");
                Console.WriteLine("12 - Delete a customer");
                Console.WriteLine("13 - Update a customer");
                Console.WriteLine("14 - Show all customers");
                Console.WriteLine("15 - Show a customer by ID");
                Console.WriteLine("16 - Add a car");
                Console.WriteLine("17 - Delete a car");
                Console.WriteLine("18 - Update a car");
                Console.WriteLine("19 - Show all cars");
                Console.WriteLine("20 - Show a car by ID");
                Console.WriteLine("'exit' - Exit the program");

                Console.Write("Enter the command number: ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting the program...");
                    break;
                }

                switch (input)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        DeleteEmployee();
                        break;
                    case "3":
                        UpdateEmployee();
                        break;
                    case "4":
                        ShowAllEmployees();
                        break;
                    case "5":
                        ShowEmployeeById();
                        break;
                    case "6":
                        AddSale();
                        break;
                    case "7":
                        DeleteSale();
                        break;
                    case "8":
                        UpdateSale();
                        break;
                    case "9":
                        ShowAllSales();
                        break;
                    case "10":
                        ShowSaleById();
                        break;
                    case "11":
                        AddCustomer();
                        break;
                    case "12":
                        DeleteCustomer();
                        break;
                    case "13":
                        UpdateCustomer();
                        break;
                    case "14":
                        ShowAllCustomers();
                        break;
                    case "15":
                        ShowCustomerById();
                        break;
                    case "16":
                        AddCar();
                        break;
                    case "17":
                        DeleteCar();
                        break;
                    case "18":
                        UpdateCar();
                        break;
                    case "19":
                        ShowAllCars();
                        break;
                    case "20":
                        ShowCarById();
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }



        public IEnumerable<Employee> GetAllEmployees() => _context.Employees.AsNoTracking().ToList();
    }
}
