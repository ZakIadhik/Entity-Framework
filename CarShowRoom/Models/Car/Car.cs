using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models;

public class Car
{
    [Key]
    public int CarId { get; set; } 
    
    [Required]
    [StringLength(50)]
    public string Brand { get; set; } 
    
    [Required]
    [StringLength(50)]
    public string Model { get; set; } 
    
    [Required]
    [Range(1880, 2025, ErrorMessage = "The year of manufacture must be between 1880 and 2025.")]
    public int Year { get; set; } 
    
    [Required]
    [Range(0, 1_000_000, ErrorMessage = "The price must be between 0 and 1,000,000.")]
    public decimal Price { get; set; } 
    
    [ForeignKey("CustomerId")]
    public int? CustomerId { get; set; } 
    public Customer Customer { get; set; } 
    
    [ForeignKey("CarTypeId")]
    public int CarTypeId { get; set; } 
    public CarType CarType { get; set; } 
    
    [ForeignKey("CarBrandId")]
    public int CarBrandId { get; set; } 
    public CarBrand CarBrand { get; set; } 
}