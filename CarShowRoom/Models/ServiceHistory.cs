using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowRoom.Models;

public class ServiceHistory
{
    [Key]
    public int ServiceHistoryId { get; set; } 
    
    [Required]
    public DateTime ServiceDate { get; set; } 
    
    [Required]
    [StringLength(255, ErrorMessage = "Description must not exceed 255 characters.")]
    public string ServiceDescription { get; set; } 
    
    [ForeignKey("CarId")]
    public int CarId { get; set; }
    public Car Car { get; set; } 
}