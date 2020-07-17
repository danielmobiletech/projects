using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Database;
using Shop.Domain.Enum;

namespace Shop.Application.OrdersAdmin
{
    public class GetOrders
    {
        ApplicationDbContext _context;
        public GetOrders(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Response> Do(int status)
        {
            return _context.Orders
                .Where(x => x.Status == (OrderStatus)status)
                .Select(x => new Response
                {
                    Id = x.Id,
                    OrdersRef = x.OrderRef,
                    Email = x.Email
                }).ToList();
                
        }


        public class Product
        {
            public string Value { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string StockDescription { get; set; }
            public int Qty { get; set; }
        }
        public class Response
        {
            public int Id { get; set; }
            public string OrdersRef { get; set; }
            public string Email { get; set; }

        }

    }
}
