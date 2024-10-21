using System.ComponentModel.DataAnnotations;

namespace WebRestaurantManagement.Models;

public class Order
{
    public int Id { get; set; }
    public DateOnly? OrderDateTime { get; set; }
    [Required]
    public int WaiterId { get; set; }
    public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual Waiter? Waiter { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}