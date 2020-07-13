﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;

namespace Shop.Application.Cart
{
    public class AddCustomerInformation
    {
        private ISession _session;

        public AddCustomerInformation(ISession session)
        {
            _session = session;
        }

        public void Do(Request request)
        {
           
            var stringObject = JsonConvert.SerializeObject(request);
            var customerInformation = new CustomerInformation
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address1 = request.Address1,
                Address2 = request.Address2,
                Email=request.Email,
                PhoneNumber=request.PhoneNumber,
                PostCode=request.PostCode,
                City=request.City
            };
            _session.SetString("customer-info", JsonConvert.SerializeObject(customerInformation));
        }

        public class Request
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
    }
}

