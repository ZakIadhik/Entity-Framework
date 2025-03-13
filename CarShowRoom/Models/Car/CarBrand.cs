using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models;

public class CarBrand
{
    public int CarBrandId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string BrandName { get; set; } 
}