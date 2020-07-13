using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Database;

namespace Shop.Application.Orders
{
    public class GetOrder
    {
        ApplicationDbContext _context;
        public GetOrder(ApplicationDbContext context)
        {
            _context = context;
        }


        public Response Do(string reference)
        {
            return _context.Orders
                .Where(x => x.OrderRef == reference)
                .Include(x => x.OrderStocks)
                .ThenInclude(x => x.Stock)
                .ThenInclude(x => x.Products)
                .Select(x => new Response
                {
                    OrderRef = x.OrderRef,

                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    PostCode = x.PostCode,
                    City = x.City,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Product= x.OrderStocks.Select(x=>new Product
                    {
                        Value=$"${x.Stock.Products.Value.ToString("N2")}",
                        Description=x.Stock.Products.Description,
                        Name=x.Stock.Products.Name,
                        Qty=x.Qty,
                        StockDescription=x.Stock.Description

                    }),
                    Total=x.OrderStocks.Sum(x=>x.Stock.Products.Value).ToString("N2")
                }).FirstOrDefault();
        }


        public class Product
        {
            public string Value { get; set; }
            public string Name{ get; set; }
            public string Description{ get; set; }
            public string StockDescription { get; set; }
            public int Qty { get; set; }
        }
        public class Response
        {
            public string Total { get; set; }
            public IEnumerable<Product> Product { get; set; }
            public string OrderRef { get; set; }
            public string FirstName { get; set; }

            public string LastName { get; set; }


            public string Email { get; set; }


            public string PhoneNumber { get; set; }

            public string Address1 { get; set; }

            public string Address2 { get; set; }

            public string City { get; set; }

            public string PostCode { get; set; }

        }
    }
}
