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

        public void Do(Request request)
        {
           
            
        }



        public class Response
        {
            public string Name { get; set; }
            public string Values { get; set; }
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
            var response = _context.Stocks
                 .Include(x => x.Products)
                 .Where(x => cartList.Any(m=>m.StockId==x.Id)) 
                 .Select(y => new Response
                 {
                     Name = y.Products.Name,
                     StockId = y.Id,
                     Qty = cartList.FirstOrDefault(n=>n.StockId==y.Id).Qty,
                     Values = $"${y.Products.Value.ToString("N2") }"

                 }).ToList();

            return response;
        }
        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
    
}
