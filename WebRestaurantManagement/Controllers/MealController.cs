using Microsoft.AspNetCore.Mvc;
using WebRestaurantManagement.Context;

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
}