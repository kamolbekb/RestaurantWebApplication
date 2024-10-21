using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebRestaurantManagement.Models;

public class Meal
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Describtion { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}