using System;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.ProductsAdmin
{
    public class GetProduct
    {
        private readonly ApplicationDbContext _context;
        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public ProductViewModel Do(int id)
        {
            return _context.Products.Where(m=>m.Id==id).Select(m=>new ProductViewModel {
                Id=m.Id,
                Name=m.Name,
                Description=m.Description,
                Value=m.Value

            }).FirstOrDefault();

        }
        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }
            public decimal Value { get; set; }
        }
    }
   

}
