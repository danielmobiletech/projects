using System;
using System.Collections.Generic;
using System.Linq;

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
        
            _context.Products.ToList().Select(m => new GetProducts.ProductViewModel { Name = m.Name,
            Description = m.Description,
            Value = $"${ m.Value.ToString("N2") }" });



        public class ProductViewModel
        {
            public string Description { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
   


}
