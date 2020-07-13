using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Shop.Application.Products;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public CreateProduct.ProductViewModel Product { get; set; }

        

        
        
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context )
        {
            _context = context;
        }
        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }
        public void OnGet()
        {
            Products = new GetProducts(_context).Do();
        }
        public async Task<IActionResult> OnPost()
        {
            await new CreateProduct(_context).Do(Product);
            
            return RedirectToPage("index");
        }
    }
}
