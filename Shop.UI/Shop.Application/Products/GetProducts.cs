using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Shop.Application.Products
{
    public class GetProducts
    {
        private readonly Database.ApplicationDbContext _context;
        public GetProducts(Database.ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductViewModel> Do() =>
        
            _context.Products
            .Include(x=>x.Stocks)
            .Select(m => new GetProducts.ProductViewModel
            {
                Name = m.Name,

                Description = m.Description,

                Value = $"${ m.Value.ToString("N2") }",
                StockCount=m.Stocks.Sum(x=>x.Qty)
                

            }).ToList();



        public class ProductViewModel
        {
            public string Description { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
            public int StockCount { get; set; }
        }
    }
   


}
