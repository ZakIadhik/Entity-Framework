using System.ComponentModel.DataAnnotations;

namespace EfCore.Models;

public class Customer
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    
    [StringLength(50)]
    public string LastName { get; set; }  

    [Required]
    [StringLength(50)]
    public string Email { get; set; }
    

    [StringLength(20)]
    public string PhoneNumber { get; set; }  

    public virtual List<Order> Orders { get; set; } = new List<Order>();
}