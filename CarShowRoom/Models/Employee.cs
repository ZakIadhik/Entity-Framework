using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models;

public class Employee
{
    [Key]
    public int EmployeeId { get; set; } 
    
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } 
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } 
    
    [Required]
    [Range(500, 1_000_000, ErrorMessage = "Salary must be between 500 and 1,000,000.")]
    public decimal Salary { get; set; } 
    
    public ICollection<Sale> Sales { get; set; } 
}