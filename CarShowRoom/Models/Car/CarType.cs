using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models;

public class CarType
{
    public int CarTypeId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string TypeName { get; set; } 
}