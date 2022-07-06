using AdventureWorks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("product-linetotal-sum-by-color")]
        public async Task<IActionResult> GetProductLineTotalByColor()
        {
            var products = _context.Products.GroupBy(x => x.Color,
                (k, v) => new
                {
                    Color = k,
                    LineTotalSum = _context.SalesOrderDetails
                                    .Where(x => x.Product.Color == k)
                                    .Sum(x => x.LineTotal)
                });

            return Ok(products);
        }
    }
}
