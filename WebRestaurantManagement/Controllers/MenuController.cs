using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Context;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class MenuController
{
    private readonly MyDbContext _context;

    public MenuController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet("All menus")]
    public List<Menu> GetMenus()
    {
        return _context.Menus.ToList();
    }

    [HttpGet("Id")]
    public Menu GetMenuByIdAsync(int id)
    {
        var menu =  _context.Menus.FirstOrDefault(m => m.Id == id);
        return menu;
    }

    [HttpPost]
    public async Task<string> PostMenuAsync(Menu menu)
    {
        if (menu == null)
        {
            return "Meal can't be null!";
        }

        _context.Menus.Add(menu);
        await _context.SaveChangesAsync();
        return "Success!";
    }

    [HttpPut("{id}")]
    public async Task<string> UpdateMenu(int id, Menu menu)
    {
        if (id != menu.Id)
        {
            return "Meal Id does not match.";
        }

        _context.Entry(menu).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return $"Meal with Id {id} not found: {e.Message}";
        }
        catch (Exception e)
        {
            return  $"Internal server error: {e.Message}";
        }

        return "Success"; // Возвращает 204 No Content при успешном обновлении
    }

    [HttpDelete("Id")]
    public async Task<string> DeleteMenuAsync(int id)
    {
        var menu = await _context.Menus.FirstOrDefaultAsync(i => i.Id == id);
        if (menu == null)
        {
            return "Not found";
        }

        _context.Menus.Remove(menu);
        await _context.SaveChangesAsync();

        return "Success";
    }
}