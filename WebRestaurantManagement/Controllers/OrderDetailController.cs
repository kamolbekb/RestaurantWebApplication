using Microsoft.AspNetCore.Mvc;
using WebRestaurantManagement.Context;

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
}