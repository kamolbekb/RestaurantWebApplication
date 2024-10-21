namespace WebRestaurantManagement.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}