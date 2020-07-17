using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Shop.Application.Orders;
using Shop.Application.OrdersAdmin;
using Shop.Database;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.UI.Controllers
{

    [Authorize(Policy = "Manager")]
    [Authorize(Policy = "Admin")]
    [Route("[controller]")]
    public class OrdersController : Controller
    {

        private ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        [HttpGet("")]
        public IActionResult GetOrders(int stats,[FromServices]GetOrders getOrders) => Ok( getOrders.Do(stats));


        [HttpGet("{id}")]
        public IActionResult GetOrder(int id, [FromServices] GetOrder getOrder) => Ok(getOrder.Do(id));



       


        


        [HttpPut("")]
        public async Task<IActionResult> UpdateOrder(int id,[FromServices] UpdateOrder updateOrder) => Ok(await updateOrder.DoAsync(id));





    }
}
