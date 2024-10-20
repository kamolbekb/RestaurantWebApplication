namespace WebRestaurantManagement.Models;

public class Order
{
    public int Id { get; set; }
    public DateOnly? OrderDateTime { get; set; }
    public int? WaiterId { get; set; }
    public virtual Waiter? Waiter { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}