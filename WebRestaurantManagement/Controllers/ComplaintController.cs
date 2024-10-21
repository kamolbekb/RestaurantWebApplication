using Microsoft.AspNetCore.Mvc;
using WebRestaurantManagement.Context;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class ComplaintController
{
    private readonly MyDbContext _context;

    public ComplaintController(MyDbContext context)
    {
        _context = context;
    }
}