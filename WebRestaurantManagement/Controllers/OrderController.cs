using Microsoft.AspNetCore.Mvc;
using WebRestaurantManagement.Context;

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
}