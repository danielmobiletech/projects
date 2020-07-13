using System;
using System.Collections.Generic;
using System.Linq;
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
        public ProductViewModel Do(string name)
        {
            _context.StockOnHolds.Where(x => x.ExpireDate < DateTime.Now).ToList();

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
                    InStock = (y.Qty > 0)


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
            public bool InStock { get; set; }
        }
    }
}
