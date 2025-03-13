using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } 
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } 
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } 
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    
    public ICollection<Car> Cars { get; set; }
    
    public ICollection<Sale> Sales { get; set; }

}