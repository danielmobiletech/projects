using System;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private readonly ApplicationDbContext _context;
        public DeleteProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Do(int id)
        {
            var Product = _context.Products.FirstOrDefault(m => m.Id == id);
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    public class ProductViewModel
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}

