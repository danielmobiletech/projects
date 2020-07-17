using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI
{
    public class CartModel : PageModel
    {
        private ApplicationDbContext _context;
        public CartModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<GetCart.Response> CartList {get;set; }
        public IActionResult OnGet()
        {
            CartList = new GetCart(HttpContext.Session, _context).Do();
            return Page();
        }
    }
}
