using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebRestaurantManagement.Models;

public class Order
{
    public int Id { get; set; }
    public DateOnly? OrderDateTime { get; set; }
    [Required]
    public int WaiterId { get; set; }
    public int? CustomerId { get; set; }
    [JsonIgnore]
    public int? ReportId { get; set; }
    [JsonIgnore]
    public int? RecipeId { get; set; }
    public virtual Recipe? Recipe { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual Waiter? Waiter { get; set; }
    [JsonIgnore]
    public virtual Report? Report { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    
}