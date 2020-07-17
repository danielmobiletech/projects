using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.StockAdmin;
using Shop.Database;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.UI.Controllers
{
  // [Authorize(Policy = "Manager")]
    //[Authorize(Policy = "Admin")]
    [Route("[controller]")]
    public class StocksController : Controller
    {
        // GET: /<controller>/
        private ApplicationDbContext _context;
        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("")]
        public IActionResult GetStocks() => Ok(new GetStocks(_context).Do());




        [HttpPost("")]
        public async Task<IActionResult> CreateStock([FromBody] CreateStock.Request pvm) => Ok(await new CreateStock(_context).Do(pvm));




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id) => Ok(await new DeleteStock(_context).Do(id));



        [HttpPut("")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStock.Request pvm) => Ok(await new UpdateStock(_context).Do(pvm));
    }
}
