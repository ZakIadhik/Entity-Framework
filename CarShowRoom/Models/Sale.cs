using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowRoom.Models;

public class Sale
{
    [Key]
    public int SaleId { get; set; }
    
    [Required]
    public DateTime SaleDate { get; set; }
    
    [Required]
    [Range(0, 1_000_000, ErrorMessage = "Sale price must be between 0 and 1,000,000.")]
    public decimal SalePrice { get; set; } 

    [ForeignKey("CarId")]
    public int CarId { get; set; }
    public Car Car { get; set; } 
    
    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } 
    
    [ForeignKey("EmployeeId")]
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } 
}