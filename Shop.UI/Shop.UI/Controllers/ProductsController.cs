using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Database;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.UI.Controllers
{
    //[Authorize(Policy = "Manager")]
    //[Authorize(Policy = "Admin")]
    [Route("[controller]")] 
    public class ProductsController : Controller
    {
        // GET: /<controller>/

        private ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        [HttpGet("")]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());


        [HttpGet("{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));



        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request pvm) => Ok(await new CreateProduct(_context).Do(pvm));




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok(await new DeleteProduct(_context).Do(id));



        [HttpPut("")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request pvm) => Ok(await new UpdateProduct(_context).Do(pvm));

    }
}
