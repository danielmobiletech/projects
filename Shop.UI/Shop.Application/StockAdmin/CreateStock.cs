using System;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.StockAdmin
{
    public class CreateStock
    {
        private ApplicationDbContext _context;
        public CreateStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Do(Request req)
        {
            var stock = new Stock
            {
                Qty = req.Qty,
                Description = req.Description,
                ProductId = req.ProductId
            };
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return new Response
            {
                Id = stock.Id,
                Description=stock.Description,
                Qty=stock.Qty

            };
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
