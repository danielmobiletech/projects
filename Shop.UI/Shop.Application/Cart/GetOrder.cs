using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;

namespace Shop.Application.Cart
{
    public class GetOrder
    {

        private ISession _session;
        ApplicationDbContext _context;
        public GetOrder(ISession session,ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

         public Response Do()
        {
            var cart = _session.GetString("cart");
            

            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cart);

            var listOfProducts = _context.Stocks
                .Include(x => x.Products)
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Qty = cartList.FirstOrDefault(b=>b.StockId==x.Id).Qty,
                    Value = (int)(x.Products.Value * 100)

                }).ToList();
            var customerString = _session.GetString("customer-info");
            var customerInformation = JsonConvert.DeserializeObject<CustomerInformation>(customerString);
             return new Response
            {
                Products = listOfProducts,
                CustomerInformation=new CustomerInformation
                {
                    FirstName = customerInformation.FirstName,
                    LastName = customerInformation.LastName,
                    Address1 = customerInformation.Address1,
                    Address2 = customerInformation.Address2,
                    Email = customerInformation.Email,
                    PhoneNumber = customerInformation.PhoneNumber,
                    PostCode = customerInformation.PostCode,
                    City = customerInformation.City
                },

            };
        }
        public class Product
        {
            public int ProductId { get; set; }
            public int Qty { get; set; }
            public int StockId { get; set; }
            public int Value { get; set; }
        }

        public class Customer
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }
            [Required]
            public string Address1 { get; set; }


            public string Address2 { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string PostCode { get; set; }
        }
        public class Response
        {
            
            public IEnumerable<Product> Products{ get; set; }
            public CustomerInformation CustomerInformation { get; set; }
            public int GetTotalCharge() => Products.Sum(x => x.Value * x.Qty);
        }
    }
}
