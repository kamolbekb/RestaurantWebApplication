namespace WebRestaurantManagement.Models;

public class Recipe
{
    public int WaiterId { get; set; }
    public int OrderId { get; set; }
    public int MealId { get; set; }
    public double UnitPrice { get; set; }
    public int Count { get; set; }
    public double TotalPrice { get; set; }
    public virtual Order Order { get; set; } = null!;
    public virtual Meal Meal { get; set; } = null!;
}