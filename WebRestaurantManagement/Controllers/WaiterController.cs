using Microsoft.AspNetCore.Mvc;
using WebRestaurantManagement.Context;

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
}