namespace WebRestaurantManagement.Models;

public class Menu
{
    public string? Description { get; set; }
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}