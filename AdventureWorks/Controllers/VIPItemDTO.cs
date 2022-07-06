using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Controllers
{
    public class VIPItemDTO
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal AverageTotalDue { get; set; }
    }
}