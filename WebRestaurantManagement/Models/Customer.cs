using System.ComponentModel.DataAnnotations;

namespace WebRestaurantManagement.Models;

public class Customer
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
}