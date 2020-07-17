using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;
        private ApplicationDbContext _context;
        public AddToCart(ISession session, ApplicationDbContext context)
        {
            this._context = context;

            _session = session;
        }
        
        public async Task<bool> Do(Request request)
        {


            var stocOnkHold = _context.StockOnHolds.Where(x => x.SessionId == _session.Id).ToList();
            var stockHold = _context.Stocks.Where(x => x.Id == request.StockId).FirstOrDefault();
            if (stockHold.Qty<request.Qty)
            {
                //not enough stock
                return false;
            }


            _context.StockOnHolds.Add(new StockOnHold
            {

                StockId = request.StockId,
                SessionId=_session.Id, 
                Qty = request.Qty,
                ExpireDate = DateTime.Now.AddMinutes(20)


            }) ;

            stockHold.Qty = stockHold.Qty-request.Qty;

            foreach(var stock in stocOnkHold)
            {
                 
                stock.ExpireDate = DateTime.Now.AddMinutes(20);
            }

            await _context.SaveChangesAsync();


            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString("cart");
            if(!string.IsNullOrEmpty(stringObject))
            {
               cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if(cartList.Any(x=>x.StockId==request.StockId))
            {
                cartList.Find(x => x.StockId == request.StockId).Qty += request.Qty;

            }

            else
            {
                cartList.Add(new CartProduct
                {
                    Qty = request.Qty,
                    StockId = request.StockId
                }); 
            }


           
            //cartList.Add(cartProduct);
             stringObject = JsonConvert.SerializeObject(cartList);
            _session.SetString("cart", stringObject);
            return true;
        }
        
        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
