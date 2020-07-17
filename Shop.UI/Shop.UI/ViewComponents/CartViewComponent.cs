using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.ViewComponents
{
    public class CartViewComponent:ViewComponent
    {
        private ApplicationDbContext _context;
        public CartViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(string view="Default")
        {
            if(view=="Small")
            {

                var totalvalue = new GetCart(HttpContext.Session, _context).Do().Sum(x => x.Qty * x.TotalValue);
                return View(view,  $"${totalvalue}");
            }

            return View(view,new GetCart(HttpContext.Session, _context).Do());
        }
    }
}
