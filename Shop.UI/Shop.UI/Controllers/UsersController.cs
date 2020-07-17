using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Application.StockAdmin;
using Shop.Application.UsersAdmin;
using Shop.Database;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.UI.Controllers
{
    [Authorize(Policy="Admin")]
    [Authorize(Policy = "Admin")]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private CreateUser _createUser;
        public UsersController(CreateUser createUser)
        {
            _createUser = createUser;
        }
       public async Task<IActionResult> CreateUser([FromBody]CreateUser.Request request)
        {
            await _createUser.Do(request);
            return Ok();

        }




        /*
        // GET: /<controller>/
        [HttpGet("products")]
        public IActionResult GetProducts() => Ok(new GetProducts(_context).Do());


        [HttpGet("products/{id}")]
        public IActionResult GetProduct(int id) => Ok(new GetProduct(_context).Do(id));



        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProduct.Request pvm) => Ok(await new CreateProduct(_context).Do(pvm));




        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id) => Ok(await new DeleteProduct(_context).Do(id));



        [HttpPut("products")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProduct.Request pvm) => Ok(await new UpdateProduct(_context).Do(pvm));



        //stocks


        [HttpGet("stocks")]
        public IActionResult GetStocks() => Ok(new GetStocks(_context).Do());




        [HttpPost("stocks")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request pvm) => Ok(await new CreateStock(_context).Do(pvm));




        [HttpDelete("stocks/{id}")]
        public async Task<IActionResult> DeleteStock(int id) => Ok(await new DeleteStock(_context).Do(id));



        [HttpPut("stocks")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request pvm) => Ok(await new UpdateStock(_context).Do(pvm));
        */


    }
}
