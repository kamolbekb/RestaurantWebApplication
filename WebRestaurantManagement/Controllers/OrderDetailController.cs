using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Context;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderDetailController : ControllerBase
{
    private readonly MyDbContext _context;

    public OrderDetailController(MyDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("All")]
    public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
    {
        return await _context.OrderDetails
            .Include(od => od.Order)
            .Include(od => od.Meal)
            .ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
    {
        var orderDetail = await _context.OrderDetails
            .Include(od => od.Order)
            .Include(od => od.Meal)
            .FirstOrDefaultAsync(od => od.OrderId == id);

        if (orderDetail == null)
        {
            return NotFound();
        }

        return orderDetail;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDetail>> CreateOrderDetail(OrderDetail orderDetail)
    {
        if (orderDetail == null)
        {
            return BadRequest("OrderDetail cannot be null.");
        }

        _context.OrderDetails.Add(orderDetail);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.OrderId }, orderDetail);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetail orderDetail)
    {
        if (id != orderDetail.OrderId)
        {
            return BadRequest("OrderDetail ID mismatch.");
        }

        _context.Entry(orderDetail).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return NotFound($"Order Details with Id {id} not found: {e.Message}");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderDetail(int id)
    {
        var orderDetail = await _context.OrderDetails.FindAsync(id);
        if (orderDetail == null)
        {
            return NotFound();
        }

        _context.OrderDetails.Remove(orderDetail);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}