using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Context;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Controllers;
[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly MyDbContext _context;

    public CustomerController(MyDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("All Customer")]
    public List<Customer> GetCustomers()
    {
        return _context.Customers.ToList();
    }

    [HttpGet("Id")]
    public Customer GetCustomerByIdAsync(int id)
    {
        var customer =  _context.Customers.FirstOrDefault(m => m.Id == id);
        return customer;
    }

    [HttpPost]
    public async Task<string> PostCustomersAsync(Customer customer)
    {
        if (customer == null)
        {
            return "Customer can't be null!";
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return "Success!";
    }

    [HttpPut("{id}")]
    public async Task<string> UpdateComplaint(int id, Customer customer)
    {
        if (id != customer.Id)
        {
            return "Customer Id does not match.";
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            return $"Customer with Id {id} not found: {e.Message}";
        }
        catch (Exception e)
        {
            return  $"Internal server error: {e.Message}";
        }

        return "Success"; // Возвращает 204 No Content при успешном обновлении
    }

    [HttpDelete("Id")]
    public async Task<string> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(i => i.Id == id);
        if (customer == null)
        {
            return "Not found";
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return "Success";
    }
}