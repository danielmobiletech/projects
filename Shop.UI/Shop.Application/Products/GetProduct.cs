using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Database;

namespace Shop.Application.Products
{
    public class GetProduct
    {
        private  ApplicationDbContext _context;
        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProductViewModel> Do(string name)
        {
            var stockOnHold=_context.StockOnHolds.Where(x => x.ExpireDate < DateTime.Now).ToList();
            if(stockOnHold.Count>0)
            {
                var stockToReturn = new List<Domain.Models.Stock>();
                foreach(var holds in stockOnHold)
                {
                    if(_context.Stocks.Any(x=>x.Id==holds.Id))
                    {
                        stockToReturn.Add(_context.Stocks.FirstOrDefault(x => x.Id == holds.Id));
                        //break;
                    }
                }
                /*
                var stockToReturn = _context.Stocks.Where(x => stockOnHold.Any(y => y.StockId == x.Id)).ToList();
                */
                foreach (var stock in stockToReturn)
                {
                    stock.Qty = stock.Qty + stockOnHold.FirstOrDefault(x => x.StockId == stock.Id).Qty;
                }

                _context.StockOnHolds.RemoveRange(stockOnHold);
                await _context.SaveChangesAsync();
            }


            return _context.Products
           .Include(x => x.Stocks)
           .Where(m => m.Name == name)
           .Select(x => new ProductViewModel
           {
               Description = x.Description,
               Name = x.Name,
               Value = $"${x.Value.ToString("N2")}",
               Stock = x.Stocks.Select(y =>
                new StockViewModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    Qty=y.Qty


                })
           }
           ).FirstOrDefault();
        }

        public class ProductViewModel
        {
            public string Description { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
           // public bool InStock { get; set; }
           
        }
    }
}
