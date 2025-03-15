using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Models;

public class Car
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Make { get; set; } = string.Empty;

    [Required]
    public string Model { get; set; } = string.Empty;
    
    [Range(1900, 2100)]
    public int Year { get; set; }
    

    
    public int? DealerId { get; set; }
    
    public virtual Dealer Dealer { get; set; }  
    
    public virtual List<Order> Orders { get; set; } = new List<Order>();
    
    public bool IsDeleted { get; set; }
    
}
