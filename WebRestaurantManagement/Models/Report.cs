namespace WebRestaurantManagement.Models;

public class Report
{
    public int OrdersCount { get; set; }
    public double TotalSales { get; set; }
    public int ComplaintsCount { get; set; }
    public DateOnly? SalesDay { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}