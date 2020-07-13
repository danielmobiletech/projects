using System;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;
using System.Collections.Generic;

namespace Shop.Application.StockAdmin
{
    public class UpdateStock
    {
        private  ApplicationDbContext _context;
        public UpdateStock(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Do(Request req)
        {

            var stocks = new List<Stock>();
            foreach(var stock in req.Stock.ToList())
            {
                stocks.Add(new Stock {
                    Id=stock.Id,
                    ProductId=stock.ProductId,
                    Description=stock.Description,
                    Qty=stock.Qty

                });
            }
            _context.Stocks.UpdateRange(stocks);
            await _context.SaveChangesAsync();
            return new Response
            {
                 Stock= req.Stock
            };
        }
        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
        public class Request
        {
           public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class Response
        {
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
