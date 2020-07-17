using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.Products;
using Shop.Database;


namespace Shop.UI.Pages
{
    public class ProductModel : PageModel
    {
        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public Test ProductTest { get; set; }

        public class Test
        {
            public string Id { get; set; }
        }
        private readonly ApplicationDbContext _context;
        public ProductModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public GetProduct.ProductViewModel Product { get; set; }
        public async Task<IActionResult> OnGet(string name)
        {
            Product = await new GetProduct(_context).Do(name.Replace("-", " "));
            if (Product == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {

            var result = await new AddToCart(HttpContext.Session, _context).Do(CartViewModel);
            if (result)
            {
                return RedirectToPage("Cart");
            }
            else return Page();

        }
    }
}