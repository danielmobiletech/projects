using System;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Cart;
using Shop.Application.Orders;
using Shop.Application.UsersAdmin;

namespace Shop.Application
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {

           /* @this.AddTransient<CreateUser>();
            @this.AddTransient<AddCustomerInformation>();
            @this.AddTransient<AddToCart>();
            @this.AddTransient<GetCart>();
            @this.AddTransient<GetCustomerInformation>();
            @this.AddTransient<Cart.GetOrder>();


            @this.AddTransient<Orders.GetOrder>();
            @this.AddTransient<CreateOrder>();
           */


            @this.AddTransient<OrdersAdmin.GetOrder>();
            @this.AddTransient<OrdersAdmin.GetOrders>();
            @this.AddTransient<OrdersAdmin.UpdateOrder>();

            /*@this.AddTransient<Products.CreateProduct>();
            @this.AddTransient<Products.DeleteProduct>();
            @this.AddTransient<Products.GetProduct>();
            @this.AddTransient<Products.GetProducts>();
            @this.AddTransient<Products.UpdateProduct>();

            @this.AddTransient<ProductsAdmin.CreateProduct>();
            @this.AddTransient<ProductsAdmin.DeleteProduct>();
            @this.AddTransient<ProductsAdmin.GetProduct>();
            @this.AddTransient<ProductsAdmin.GetProducts>();
            @this.AddTransient<ProductsAdmin.UpdateProduct>();

            @this.AddTransient<StockAdmin.CreateStock>();
            @this.AddTransient<StockAdmin.DeleteStock>();
            @this.AddTransient<StockAdmin.GetStocks>();
            @this.AddTransient<StockAdmin.UpdateStock>();


            @this.AddTransient<CreateUser>();
            @this.AddTransient<CreateUser>();*/
            @this.AddTransient<CreateUser>();


            return @this;
        }
    }
}
