using System.Diagnostics.CodeAnalysis;

namespace WebRestaurantManagement.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? MealsCount { get; set; }
    public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
}