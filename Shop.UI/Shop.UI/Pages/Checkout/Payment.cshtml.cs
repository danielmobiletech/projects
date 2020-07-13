using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.Cart;
using Shop.Application.Orders;
using Shop.Database;
using Stripe;

namespace Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        ApplicationDbContext _context;
        public PaymentModel(IConfiguration config,ApplicationDbContext context)
        {
            PublicKey = config["Stripe:PublicKey"].ToString();
            _context = context;
        }

        public string PublicKey { get; }

        public IActionResult OnGet()
        {
            var Information = new GetCustomerInformation(HttpContext.Session).Do();
            if (Information == null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");

            }
            return Page();
            
        }

        public async Task<IActionResult> OnPost(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            var CartOrder = new Application.Cart.GetOrder(HttpContext.Session, _context).Do();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source =stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = CartOrder.GetTotalCharge(),
                Description = "Sample Charge",
                Currency = "usd",
                Customer = customer.Id
            });

            await new CreateOrder(_context).Do(new CreateOrder.Request
            {
                StripeRef=charge.Id,
                FirstName=CartOrder.CustomerInformation.FirstName,
                LastName = CartOrder.CustomerInformation.LastName,
                Address1 = CartOrder.CustomerInformation.Address1,
                Address2 = CartOrder.CustomerInformation.Address2,
                PostCode = CartOrder.CustomerInformation.PostCode,
                City= CartOrder.CustomerInformation.City,
                Email= CartOrder.CustomerInformation.Email,
                PhoneNumber= CartOrder.CustomerInformation.PhoneNumber,
                Stocks= (CartOrder.Products.Select(x=>new CreateOrder.Stock
                {
                    StockId=x.StockId,
                    Qty=x.Qty
                }).ToList())

            });

            return RedirectToPage("/index");
        }
    }
}
