using Proekt.DataBase;
using Proekt.Models;

namespace Proekt.Services;

public class UserService
{
    private readonly GameStoreContext _context;

    public UserService(GameStoreContext context)
    {
        _context = context;
    }

    public User RegisterUser(string name, string email, string password, Roles role = Roles.User)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password); 

        var user = new User 
        { 
            Name = name, 
            Email = email, 
            Password = hashedPassword, 
            Balance = 0, 
            Role = role
        };
    
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }
    
    public User LoginUser(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password)) 
        {
            return user;
        }
    
        return null;
    }

    public void TopUpBalance(int UserID, int Amount)
    {
        var user = _context.Users.Find(UserID);
        if (user is not null)
        {
            user.Balance += Amount;
            _context.SaveChanges();
        }
    }

    public User GetUserById(int UserID)
    {
        return _context.Users.Find(UserID);
    }

    public List<Order> GetOrderHistory(int UserID)
    {
        return _context.Orders.Where(o => o.UserId == UserID).ToList();
    }
    
    
}