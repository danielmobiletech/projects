using System;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;
namespace Shop.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private readonly ApplicationDbContext _context;
        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> Do(Request pvm)
        {
            var Product = new Product
            {

                //Id = id,
                Name = pvm.Name,
                Description = pvm.Description,
                Value = pvm.Value
            };
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return new Response
            {
                Id = Product.Id,
                Name = Product.Name,
                Description = Product.Description,
                Value=Product.Value
            };


        }

        public class Request
        {
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
