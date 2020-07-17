using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.Orders
{
    public class CreateOrder
    {
        ApplicationDbContext _context;
        public CreateOrder(ApplicationDbContext context)
        {
            _context = context;
        }
        public string CreateOrderRef()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            var result = new char[12];
            var random = new Random();
            do
            {
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = chars[random.Next(chars.Length)];
                }
            } while (_context.Orders.Any(x => x.OrderRef == new string(result)));
            return new string(result);
        }
        public async Task<bool> Do(Request request)
        {
            var stocksToUpdate = _context.StockOnHolds.Where(x => request.SessionId==x.SessionId).ToList();
            _context.StockOnHolds.RemoveRange(stocksToUpdate);

            var order = new Order
            {
                OrderRef = CreateOrderRef(),
                StripeRef=request.StripeRef,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address1 = request.Address1,
                Address2 = request.Address2,
                PostCode = request.PostCode,
                City = request.City,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                OrderStocks = (request.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Qty = x.Qty
                }).ToList())

            };
            _context.Orders.Add(order);
            return await _context.SaveChangesAsync()>0;
        }


        public class Stock
        {
            public int Qty { get; set; }
            public int StockId { get; set; }
        }
        public class Request
        {
            public string StripeRef { get; set; }
            public string FirstName { get; set; }

            public string SessionId { get; set; }
            
            public string LastName { get; set; }
            
            
            public string Email { get; set; }
            
            
            public string PhoneNumber { get; set; }
            
            public string Address1 { get; set; }


            public string Address2 { get; set; }
            
            public string City { get; set; }
            
            public string PostCode { get; set; }
            public List<Stock> Stocks { get; set; }
        }
    }
}
