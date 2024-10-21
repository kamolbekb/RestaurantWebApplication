using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Context;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly MyDbContext _context;

    public CategoryController(MyDbContext context)
    {
        _context = context;
    }
    [HttpGet("All")]
    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _context.Categories.
            Include(m=>m.Meals)
            .Select(c=>new Category
            {
                Id=c.Id,
                Name = c.Name,
                MealsCount = c.Meals.Count
            }).ToListAsync();
    }
    
    [HttpGet("All, with related tables")]
    public async Task<IEnumerable<Category>> GetCategoriesWithRelation()
    {
        return await _context.Categories
            .Include(m=>m.Meals)
            .Select(c=> new Category
            {
                Id = c.Id,
                Name = c.Name,
                MealsCount = c.Meals.Count,
                Meals = c.Meals.Select(m=>new Meal{Id = m.Id,Name = m.Name,Describtion = m.Describtion,CategoryId = m.CategoryId}).ToList()
            })
            .ToListAsync();
    }
    
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Meals)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return category;
    }
    
    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        if (category == null)
        {
            return BadRequest("Category cannot be null.");
        }

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, Category category)
    {
        if (id != category.Id)
        {
            return BadRequest("Category ID mismatch.");
        }

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return NotFound($"Category with Id {id} not found: {e.Message}");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}