namespace Proekt.Models;

public enum Roles
{
    User,
    Admin
}

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public string Password { get; set; }
    public int Balance { get; set; }
    
    public List<Order> Orders { get; set; }
    public Roles Role { get; set; }
}