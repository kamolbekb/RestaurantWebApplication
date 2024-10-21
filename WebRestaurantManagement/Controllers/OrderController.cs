using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Context;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly MyDbContext _context;

    public OrderController(MyDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }
    
    [HttpGet("All with related info")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrdersRelated()
    {
        return await _context.Orders
            .Include(c => c.OrderDetails)
            .ThenInclude(i=>i.Meal)
            .ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.Orders
            .Include(c => c.OrderDetails)
            .ThenInclude(c=>c.Meal)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (order == null)
        {
            return NotFound();
        }
        return order;
    }
    
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        if (order == null)
        {
            return BadRequest("Order cannot be null.");
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest("Order ID mismatch.");
        }

        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return NotFound($"Order with Id {id} not found: {e.Message}");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}