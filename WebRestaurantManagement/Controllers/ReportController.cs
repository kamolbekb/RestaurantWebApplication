using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Context;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly MyDbContext _context;

    public ReportController(MyDbContext context)
    {
        _context = context;
    }

    // [HttpGet("Get Profit for a period")]
    // public Report GetReportPeriod(DateTime startDate)
    // {
    //     _context.Reports.Include(o => o.Orders).Include(c => c.Complaints)
    //         .Select(r => new Report
    //         {
    //             OrdersCount = r.Orders.Count,
    //             TotalSales = r.Orders.Select(od=>od.OrderDetails.)
    //         });
    // }
}