using System.ComponentModel.DataAnnotations;

namespace WebRestaurantManagement.Models;

public class Menu
{
    [Required]
    public int Id { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}