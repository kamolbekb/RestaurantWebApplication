using System.ComponentModel.DataAnnotations;

namespace WebRestaurantManagement.Models;

public class Complaint
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    [Required] public string ComplaintSource { get; set; }
    public DateOnly Date { get; set; }
    public string? Phone { get; set; }
    public virtual Customer Customer { get; set; }
}