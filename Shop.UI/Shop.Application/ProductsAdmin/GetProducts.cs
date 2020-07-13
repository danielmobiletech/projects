using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Application.ProductsAdmin
{
    public class GetProducts
    {
        private readonly Database.ApplicationDbContext _context;
        public GetProducts(Database.ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductViewModel> Do() =>
        
            _context.Products.ToList().Select(m => new GetProducts.ProductViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Value = m.Value,
            });

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
        }

    }
    


}
