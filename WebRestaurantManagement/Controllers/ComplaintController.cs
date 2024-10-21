using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Context;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class ComplaintController : ControllerBase
{
    private readonly MyDbContext _context;

    public ComplaintController(MyDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("All Complaint")]
    public List<Complaint> GetMenus()
    {
        return _context.Complaints.ToList();
    }

    [HttpGet("Id")]
    public Complaint GetComplaintByIdAsync(int id)
    {
        var complaint =  _context.Complaints.FirstOrDefault(m => m.Id == id);
        return complaint;
    }

    [HttpPost]
    public async Task<string> PostComplaintsAsync(Complaint complaint)
    {
        if (complaint == null)
        {
            return "Complaint can't be null!";
        }

        _context.Complaints.Add(complaint);
        await _context.SaveChangesAsync();
        return "Success!";
    }

    [HttpPut("{id}")]
    public async Task<string> UpdateComplaint(int id, Complaint complaint)
    {
        if (id != complaint.Id)
        {
            return "Complaint Id does not match.";
        }

        _context.Entry(complaint).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return $"Complaint with Id {id} not found: {e.Message}";
        }
        catch (Exception e)
        {
            return  $"Internal server error: {e.Message}";
        }

        return "Success"; // Возвращает 204 No Content при успешном обновлении
    }

    [HttpDelete("Id")]
    public async Task<string> DeleteComplaintAsync(int id)
    {
        var complaint = await _context.Complaints.FirstOrDefaultAsync(i => i.Id == id);
        if (complaint == null)
        {
            return "Not found";
        }

        _context.Complaints.Remove(complaint);
        await _context.SaveChangesAsync();

        return "Success";
    }
}