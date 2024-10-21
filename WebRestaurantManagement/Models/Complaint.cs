using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebRestaurantManagement.Models;

public class Complaint
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    [Required]
    public string ComplaintSource { get; set; }
    public DateOnly Date { get; set; }
    public string? Phone { get; set; }
    [JsonIgnore] 
    public int? ReportId { get; set; }
    [JsonIgnore]
    public virtual Report? Report { get; set; }
    public virtual Customer Customer { get; set; }
}