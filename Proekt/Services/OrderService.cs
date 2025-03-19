using Proekt.DataBase;
using Proekt.Models;


namespace Proekt.Services;

public class OrderService
{
    private readonly GameStoreContext _context;

    public OrderService(GameStoreContext context)
    {
        _context = context;
    }
    
    public Order CreateOrder(int userId, List<int> gameIds)
    {
        var user = _context.Users.Find(userId);
        if (user == null)
            throw new Exception("Пользователь не найден!");

        var games = _context.Games.Where(g => gameIds.Contains(g.GameId)).ToList();
        if (!games.Any())
            throw new Exception("Игры не найдены!");

        int totalCost = games.Sum(g => g.Price);
    
        if (user.Balance < totalCost)
            throw new Exception("Недостаточно средств на балансе!");

  
        user.Balance -= totalCost;


        var order = new Order
        {
            UserId = user.UserId,
            Date = DateTime.Now,
            TotalAmount = totalCost
        };
        _context.Orders.Add(order);
        _context.SaveChanges(); 


        foreach (var game in games)
        {
            var orderStruct = new OrderStruct
            {
                OrderId = order.OrderId,
                GameId = game.GameId,
                Count = 1,
                TotalPrice = game.Price
            };
            _context.OrderStructs.Add(orderStruct);
        }

        _context.SaveChanges(); 
        return order;
    }

}