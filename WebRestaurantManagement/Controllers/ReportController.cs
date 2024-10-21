using Microsoft.AspNetCore.Mvc;
using WebRestaurantManagement.Context;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class ReportController
{
    private readonly MyDbContext _context;

    public ReportController(MyDbContext context)
    {
        _context = context;
    }
}