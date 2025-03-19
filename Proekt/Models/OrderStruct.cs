namespace Proekt.Models;

public class OrderStruct
{
    public int OrderStructId { get; set; }
    public int OrderId { get; set; }
    public int GameId { get; set; }
    public int Count { get; set; }
    public int TotalPrice { get; set; }
    
    public Order Order { get; set; }
    public Game Game { get; set; }
}