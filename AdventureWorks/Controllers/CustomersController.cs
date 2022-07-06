using AdventureWorks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("search-by-firstname")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCusotmers([FromQuery]string firstName)
        { 
            var customers = await _context.Customers
                .Where(x => x.FirstName == firstName)
                .Select(x => new CustomerDTO {
                    Id = x.CustomerId,
                    FirstName = x.FirstName,
                    LastName = x.LastName
                }).ToListAsync();
            return Ok(customers);
        }

        [HttpGet("vips")]
        public async Task<IActionResult> GetVips([FromQuery]int averageTotalDue)
        {
            var customers = await _context.Customers.Where(c => c.SalesOrderHeaders
               .GroupBy(g => g.CustomerId,
               (k, v) =>
               new
               {
                   CustomerId = k,
                   AverageTotalDue = v.Average(x => x.TotalDue)
               }).Any(x => x.AverageTotalDue > averageTotalDue)
               )
               .Select(c => new VIPItemDTO {
                CustomerId = c.CustomerId,
                CustomerName = c.FirstName + " " + c.LastName,
                AverageTotalDue = c.SalesOrderHeaders.Average(x => x.TotalDue)
               })
               .OrderByDescending(x => x.AverageTotalDue)
               .Take(5)
               .ToListAsync();
                


            //var q = await _context.SalesOrderHeaders
            //   .GroupBy(g => g.CustomerId)
            //   .Select(c =>
            //   new
            //   {
            //       CustomerId = c.Key,
            //       AverageTotalDue = c.Average(x => x.TotalDue)
            //   }).ToListAsync();

            return Ok(customers);
        }

    }
}
