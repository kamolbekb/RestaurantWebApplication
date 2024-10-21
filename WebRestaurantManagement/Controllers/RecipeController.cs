using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Context;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    private readonly MyDbContext _context;

    public RecipeController(MyDbContext context)
    {
        _context = context;
    }
    
    // [HttpGet("All")]
    // public async Task<IQueryable<Recipe>> GetRecipes()
    // {
    //     return _context.Recipes.Include(o => o.Order).Include(od => od.OrderDetails)
    //         .Select(c => new Recipe
    //         {
    //             Id = c.Id,
    //             WaiterId = c.Order.WaiterId,
    //             MealId = c.OrderDetails.
    //             UnitPrice = c.Order.OrderDetails.Select(od => od.UnitPrice),
    //             Count = c.Order.OrderDetails.Select(od=>od.Count),
    //             TotalPrice = c.Order.OrderDetails.Select(od=>od.UnitPrice*od.Count).Sum()
    //         });

        // {     
        //     Id = r.Id,
        //     WaiterId = r.Order.WaiterId,
        //     MealId = r.Order.OrderDetails.mealId
        //     UnitPrice = r.UnitPrice,
        //     Count = r.Count
        //     
        // });


        // return await _context.Categories
        //     .Include(m=>m.Meals)
        //     .Select(c=> new Category
        //     {
        //         Id = c.Id,
        //         Name = c.Name,
        //         MealsCount = c.Meals.Count,
        //         Meals = c.Meals.Select(m=>new Meal{Id = m.Id,Name = m.Name,Describtion = m.Describtion,CategoryId = m.CategoryId}).ToList()
        //     })
        //     .ToListAsync();
    // }
    //
    // [HttpGet("{id}")]
    // public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
    // {
    //     var orderDetail = await _context.OrderDetails
    //         .Include(od => od.Order)
    //         .Include(od => od.Meal)
    //         .FirstOrDefaultAsync(od => od.OrderId == id);
    //
    //     if (orderDetail == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return orderDetail;
    // }
    //
    // [HttpPost]
    // public async Task<ActionResult<OrderDetail>> CreateOrderDetail(OrderDetail orderDetail)
    // {
    //     if (orderDetail == null)
    //     {
    //         return BadRequest("OrderDetail cannot be null.");
    //     }
    //
    //     _context.OrderDetails.Add(orderDetail);
    //     await _context.SaveChangesAsync();
    //
    //     return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetail.OrderId }, orderDetail);
    // }
    //
    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetail orderDetail)
    // {
    //     if (id != orderDetail.OrderId)
    //     {
    //         return BadRequest("OrderDetail ID mismatch.");
    //     }
    //
    //     _context.Entry(orderDetail).State = EntityState.Modified;
    //
    //     try
    //     {
    //         await _context.SaveChangesAsync();
    //     }
    //     catch (DbUpdateConcurrencyException e)
    //     {
    //         return NotFound($"Order Details with Id {id} not found: {e.Message}");
    //     }
    //     catch (Exception e)
    //     {
    //         return StatusCode(500, $"Internal server error: {e.Message}");
    //     }
    //
    //     return NoContent();
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteOrderDetail(int id)
    // {
    //     var orderDetail = await _context.OrderDetails.FindAsync(id);
    //     if (orderDetail == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     _context.OrderDetails.Remove(orderDetail);
    //     await _context.SaveChangesAsync();
    //
    //     return NoContent();
    // }
}