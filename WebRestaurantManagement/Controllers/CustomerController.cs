using Microsoft.AspNetCore.Mvc;
using WebRestaurantManagement.Context;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class CustomerController
{
    private readonly MyDbContext _context;

    public CustomerController(MyDbContext context)
    {
        _context = context;
    }
}