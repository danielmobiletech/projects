using System;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        
        private readonly ApplicationDbContext _context;
        public UpdateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Do(Request pvm)
        {
            var Product = _context.Products.FirstOrDefault(x => x.Id == pvm.Id);
            Product.Id = pvm.Id;
            Product.Name = pvm.Name;
            Product.Description = pvm.Description;
            Product.Value = pvm.Value;
            _context.Products.Update(Product);
            await _context.SaveChangesAsync();

            return new Response
            {
                Id = Product.Id,
                Name = Product.Name,
                Description = Product.Description,
                Value = Product.Value
            };
        }


        



        public class Request
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
        }
    }
    
}

