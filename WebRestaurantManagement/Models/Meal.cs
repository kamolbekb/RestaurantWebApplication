using System.Diagnostics.CodeAnalysis;

namespace WebRestaurantManagement.Models;

public class Meal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Describtion { get; set; }
    public int? CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}