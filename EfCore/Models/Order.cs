namespace EfCore.Models;

public class Order
{
    
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    
    public DateTime OrderDate { get; set; }

    public virtual Car Car { get; set; }
    public virtual Customer Customer { get; set; }
}