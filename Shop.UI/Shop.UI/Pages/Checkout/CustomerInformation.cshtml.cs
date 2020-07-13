using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private ApplicationDbContext _context;
        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        IHostEnvironment _ihost;
        public CustomerInformationModel(ApplicationDbContext context, IHostEnvironment ihost)
        {
            _context = context;
            _ihost = ihost;
        }



        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            new AddCustomerInformation(HttpContext.Session).Do(CustomerInformation);
            return RedirectToPage("/Checkout/Payment");
        }


        public IActionResult OnGet()
        {
            var Information = new GetCustomerInformation(HttpContext.Session).Do();
            if (Information == null)
            {
                if (_ihost.IsDevelopment())
                {
                    CustomerInformation = new AddCustomerInformation.Request
                    {
                        FirstName = "dan",
                        LastName = "vim",
                        Address1 = "123 space",
                        Address2 = "",
                        PostCode = "11111",
                        City = "SpaceX",
                        Email = "vim@vim.com",
                        PhoneNumber = "12345678",
                    };
                    

                }

                return Page();
            }
            return RedirectToPage("/Checkout/Payment");
        }
    }
}
