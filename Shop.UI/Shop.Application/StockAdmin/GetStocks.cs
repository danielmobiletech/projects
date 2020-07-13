using System;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace Shop.Application.StockAdmin
{
    public class GetStocks
    {
        private ApplicationDbContext _context;
        public GetStocks(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductViewModel> Do()
        {
            var stocks = _context.Products
                .Include(m => m.Stocks)
                .Select(m=>new ProductViewModel
                {
                    Id=m.Id,
                    Description=m.Description,
                    Stock = m.Stocks.Select(y=>new StockViewModel
                    {
                        Id=y.Id,
                        Description=y.Description,
                        Qty=y.Qty,
                        ProductId=y.ProductId
                    })
            
                }).ToList();
            return stocks;
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }
    }
}
