using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Orders;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class OrderModel : PageModel
    {
        ApplicationDbContext _context;
        public GetOrder.Response Order {get;set;}
        public OrderModel(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void OnGet(string reference)
        {
           Order= new GetOrder(_context).Do(reference);
        }
    }
}
