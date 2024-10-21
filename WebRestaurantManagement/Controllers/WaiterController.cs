using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Context;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class WaiterController : ControllerBase
{
    private readonly MyDbContext _context;

    public WaiterController(MyDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("All Without Orders")]
    public async Task<ActionResult<IEnumerable<Waiter>>> GetWaiters()
    {
        return await _context.Waiters.ToListAsync();
    }
    
    [HttpGet("All With Orders")]
    public async Task<ActionResult<IEnumerable<Waiter>>> GetWaitersWithOrders()
    {
        return await _context.Waiters
            .Include(w => w.Orders)
            .ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Waiter>> GetWaiter(int id)
    {
        var waiter = await _context.Waiters
            .Include(w => w.Orders)
            .FirstOrDefaultAsync(w => w.Id == id);

        if (waiter == null)
        {
            return NotFound();
        }

        return waiter;
    }
    
    [HttpPost]
    public async Task<ActionResult<Waiter>> CreateWaiter(Waiter waiter)
    {
        if (waiter == null)
        {
            return BadRequest("Waiter cannot be null.");
        }

        _context.Waiters.Add(waiter);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWaiter), new { id = waiter.Id }, waiter);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWaiter(int id, Waiter waiter)
    {
        if (id != waiter.Id)
        {
            return BadRequest("Waiter ID mismatch.");
        }

        _context.Entry(waiter).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return NotFound($"Waiter with Id {id} not found: {e.Message}");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWaiter(int id)
    {
        var waiter = await _context.Waiters.FindAsync(id);
        if (waiter == null)
        {
            return NotFound();
        }

        _context.Waiters.Remove(waiter);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}