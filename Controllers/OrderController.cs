using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JoysCakeShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JoysCakeShop.Controllers
{
    [Authorize]   //--  Authorization: Can be used on the actionresult method for a more fine-grained control
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderrepository;
        private readonly ShoppingCart _shoppingcart;
        public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCart)
        {
            _orderrepository = orderRepository;
            _shoppingcart = shoppingCart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            var items = _shoppingcart.GetShoppingCartItems();
            _shoppingcart.shoppingCartItems = items;

            if(_shoppingcart.shoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some cakes you love!");
            }

            if(ModelState.IsValid)
            {
                _orderrepository.CreateOrder(order);
                _shoppingcart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            // If the checkout does not happen, the mvc framework is going to create
            // the order screen as before.  
            return View(order); 
        }

        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thank you so much for placing your order. We will notify you of your order delivery soon. " +
                                              "In case if you do not recieve anything, please contact us. ";
            return View();
        }
    }
}
