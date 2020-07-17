using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.UI.Pages.Accounts
{
    public class LoginModel : PageModel
    {


        SignInManager<IdentityUser> _signInManager;
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public LoginViewModel Input { get; set; }

        public async Task<IActionResult> OnPost()
        {

           var result= await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, false, false);
            if(result.Succeeded)
            {
                return RedirectToPage("/admin/index");
            }


            return Page();
        }

        public class LoginViewModel
        {
            public string Username { get; set; }
            public string Password { get; set; }

        }
    }
}
