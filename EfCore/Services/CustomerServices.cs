using EfCore.Models;

namespace EfCore.Services
{
    public class CustomerService
    {
        private readonly CarShowRoomContext _context;

        public CustomerService(CarShowRoomContext context)
        {
            _context = context;
        }

        public void GetAllCustomers()
        {
            var customers = _context.Customers.ToList();
            Console.WriteLine($"Total customers found: {customers.Count}");

            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.Id}, Name: {customer.FirstName} {customer.LastName}");
            }
        }


        
        private readonly List<Customer> _customers = new();

        public void AddCustomer(string firstName, string lastName, string email, string phone)
        {
            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phone
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            Console.WriteLine("Customer added successfully.");
        }



    }
}