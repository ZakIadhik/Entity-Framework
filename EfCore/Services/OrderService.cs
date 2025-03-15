using EfCore.Models;
using Microsoft.EntityFrameworkCore;

using EfCore.Services;

public class OrderService
{
    private readonly CarShowRoomContext _context;

    public OrderService(CarShowRoomContext context)
    {
        _context = context;
    }

    public void AddOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void GetAllOrders()
    {
        var orders = _context.Orders.ToList();
        Console.WriteLine($"Orders found: {orders.Count}");

        foreach (var order in orders)
        {
            Console.WriteLine($"Sale ID: {order.Id}, Car ID: {order.CarId}, Customer ID: {order.CustomerId}");
        }
    }



    public Order GetOrderById(int carId, int customerId)
    {
        return _context.Orders
            .FirstOrDefault(o => o.CarId == carId && o.CustomerId == customerId);
    }

    public void DeleteOrder(int carId, int customerId)
    {
        var order = _context.Orders.FirstOrDefault(o => o.CarId == carId && o.CustomerId == customerId);
        if (order != null)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
    
    public void MakeSale(int carId, int customerId)
    {
        var car = _context.Cars.FirstOrDefault(c => c.Id == carId);
        var customer = _context.Customers.FirstOrDefault(c => c.Id == customerId);

        if (car != null && customer != null)
        {
            var order = new Order
            {
                CarId = carId,
                CustomerId = customerId
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
    
}