namespace WebRestaurantManagement.Models;

public class Waiter
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly? HireDate { get; set; }
    public string? Address { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}