using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Context;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class MealController : ControllerBase
{
    private readonly MyDbContext _context;

    public MealController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet("All meals")]
    public async Task<IEnumerable<Meal>> GetMealsAsync()
    {
        return await _context.Meals.ToListAsync();
    }
    
    [HttpGet("All with related tables")]
    public async Task<IEnumerable<Meal>> GetMealsWithCategoriesAsync()
    {
        return await _context.Meals
            .Include(i=>i.Category)
            .ToListAsync();
    }

    [HttpGet("Id")]
    public async Task<ActionResult<Meal>> GetMealByIdAsync(int id)
    {
        var meal = await _context.Meals.FirstOrDefaultAsync(m => m.Id == id);
        if (meal == null)
        {
            return NotFound();
        }
        return meal;
    }

    [HttpPost]
    public async Task<string> PostMealAsync(Meal meal)
    {
        if (meal == null)
        {
            return "Meal can't be null!";
        }

        _context.Meals.Add(meal);
        await _context.SaveChangesAsync();
        return "Success!";
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMeal(int id, Meal meal)
    {
        if (id != meal.Id)
        {
            return BadRequest("Meal Id does not match.");
        }

        _context.Entry(meal).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return NotFound($"Meal with Id {id} not found: {e.Message}");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }

        return NoContent(); // Возвращает 204 No Content при успешном обновлении
    }

    [HttpDelete("Id")]
    public async Task<string> DeleteMealAsync(int id)
    {
        var meal = await _context.Meals.FirstOrDefaultAsync(i => i.Id == id);
        if (meal == null)
        {
            return "Not found";
        }

        _context.Meals.Remove(meal);
        await _context.SaveChangesAsync();

        return "Success";
    }
}