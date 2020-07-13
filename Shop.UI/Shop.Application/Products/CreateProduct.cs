using System;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;
namespace Shop.Application.Products
{
    public class CreateProduct
    {
        private readonly ApplicationDbContext _context;
        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task Do( ProductViewModel pvm)
        {
            _context.Products.Add(new Product
            {

                //Id = id,
                Name = pvm.Name,
                Description= pvm.Description,
                Value= pvm.Value
            });
            await _context.SaveChangesAsync();




        }




        public class ProductViewModel
        {
            public string Description { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
        }
    }


    
}
