using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.Cart
{
    public class GetCart
    {
        
        private ISession _session;
        private ApplicationDbContext _context;
        public GetCart(ISession session,ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

        



        public class Response
        {
            public string Name { get; set; }
            public string Values { get; set; }
            public decimal TotalValue { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }
        }


        public IEnumerable<Response> Do()
        {
            var stringObj = _session.GetString("cart");
            if(string.IsNullOrEmpty(stringObj))
            {
                return new List<Response>();
            }

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObj);
            var resp = new List<Response>();

            var response = _context.Stocks
                 .Include(x => x.Products);
                /* .Where(x => cartList.Any(y => y.StockId == x.Id))
                 .Select(x => new Response
                 {
                     Name = x.Products.Name,
                     StockId = x.Id,
                     TotalValue = x.Products.Value,
                     Qty = cartList.FirstOrDefault(n => n.StockId == x.Id).Qty,
                     Values = $"${x.Products.Value.ToString("N2") }"

                 }).ToList();*/
                 
            foreach (var cart in cartList)
            {
                if(response.Any(x=>x.Id==cart.StockId))
                {
                    var res = response.FirstOrDefault(x => x.Id == cart.StockId);

                    resp.Add(new Response
                    {
                        Name = res.Products.Name,
                        StockId = res.Id,
                        TotalValue = res.Products.Value,
                        Qty = cartList.FirstOrDefault(n => n.StockId == res.Id).Qty,
                        Values = $"${res.Products.Value.ToString("N2") }"
                    }); 


                }

            }
            

            return resp;
        }
        
    }
    
}
