using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebRestaurantManagement.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public int? MealsCount { get; set; }
    public int MenuId { get; set; }
    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
    public virtual Menu Menu { get; set; } 
}