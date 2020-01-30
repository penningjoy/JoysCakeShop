using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JoysCakeShop.Models;
using JoysCakeShop.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace JoysCakeShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICakeRepository _cakerepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ICakeRepository cakerepository, ShoppingCart shoppingCart)
        {
            _cakerepository = cakerepository;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.shoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.shoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int cakeId)
        {
            var selectedCake = _cakerepository.AllCakes.FirstOrDefault(c => c.CakeId == cakeId);

            if (selectedCake != null)
            {
                _shoppingCart.AddtoCart(selectedCake, 1);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int cakeId)
        {
            var selectedCake = _cakerepository.AllCakes.FirstOrDefault(c => c.CakeId == cakeId);

            if(selectedCake != null)
            {
                _shoppingCart.RemoveCart(selectedCake.CakeId);
            }

            return RedirectToAction("Index");
        }


    }
}
