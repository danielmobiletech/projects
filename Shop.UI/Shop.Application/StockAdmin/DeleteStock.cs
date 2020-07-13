using System;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;
namespace Shop.Application.StockAdmin
{
    public class DeleteStock
    {
        private readonly ApplicationDbContext _context;
        public DeleteStock(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Do(int id)
        {
            var item = _context.Stocks.FirstOrDefault(m => m.Id == id);
            _context.Stocks.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public class Request
        {
            public int Qty { get; set; }
            public string Description { get; set; }
            public int ProductId { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}
