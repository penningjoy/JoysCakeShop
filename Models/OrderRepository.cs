using System;
using System.Collections.Generic;

namespace JoysCakeShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CakesDbContext _cakesdbcontext;
        private readonly ShoppingCart _shoppingcart;

        public OrderRepository(CakesDbContext cakesdbcontext, ShoppingCart shoppingcart)
        {
            _cakesdbcontext = cakesdbcontext;
            _shoppingcart = shoppingcart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderDate = DateTime.Now;

            var shoppingCartItems = _shoppingcart.shoppingCartItems;
            order.OrderTotal = _shoppingcart.shoppingCartTotal();
            order.OrderDetails = new List<OrderDetail>();

            foreach(var shoppingcartitem in shoppingCartItems)
            {
                var orderdetails = new OrderDetail
                {
                    OrderId = order.OrderId,
                    CakeId = shoppingcartitem.cake.CakeId,
                    Amount = shoppingcartitem.Amount,
                    Price = shoppingcartitem.cake.Price,

                };

                order.OrderDetails.Add(orderdetails);
                _cakesdbcontext.OrderDetails.Add(orderdetails);
            }
            _cakesdbcontext.Orders.Add(order);
            _cakesdbcontext.SaveChanges();
        }
    }
}
