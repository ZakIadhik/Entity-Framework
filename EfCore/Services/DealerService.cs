using EfCore.Models;
namespace CarShowRoom.Services;

public class DealerService
{
    private readonly CarShowRoomContext _context;

    public DealerService(CarShowRoomContext context)
    {
        _context = context;
    }
    
    public int AddDealer(string name, string location)
    {
        var dealer = new Dealer { Name = name, Location = location };
        _context.Dealers.Add(dealer);
        _context.SaveChanges();
        return dealer.Id; 
    }

    public List<Dealer> GetAllDealers()
    {
        return _context.Dealers.ToList();
    }

    public Dealer? GetDealerById(int id)
    {
        return _context.Dealers.FirstOrDefault(d => d.Id == id);
    }

    


    public void UpdateDealer(int id, string name, string location)
    {
        var dealer = _context.Dealers.Find(id);
        if (dealer == null)
        {
            Console.WriteLine("Error: Dealer not found.");
            return;
        }

        dealer.Name = name;
        dealer.Location = location;
        _context.SaveChanges();

        Console.WriteLine("Dealer information updated successfully.");
    }


    public void DeleteDealer(int id)
    {
        var dealer = _context.Dealers.Find(id);
        if (dealer != null)
        {
            _context.Dealers.Remove(dealer);
            _context.SaveChanges();
            Console.WriteLine("Dealer deleted successfully.");
        }
        else
        {
            Console.WriteLine("Error: Dealer not found.");
        }
    }


    public bool AnyDealersExist()
    {
        return _context.Dealers.Any();
    }

    public void ListDealers()
    {
        var dealers = _context.Dealers.ToList();
        if (dealers.Count == 0)
        {
            Console.WriteLine("No registered dealers.");
            return;
        }

        Console.WriteLine("List of dealers:");
        foreach (var dealer in dealers)
        {
            Console.WriteLine($"ID: {dealer.Id}, Name: {dealer.Name}, Location: {dealer.Location}");
        }
    }

}