using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JoysCakeShop.Models
{
    public class ShoppingCart
    {
        private readonly CakesDbContext _cakedbcontext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItems { get; set; }

        public ShoppingCart (CakesDbContext cakesDbContext)
        {
            _cakedbcontext = cakesDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                                  .HttpContext.Session;

            var context = services.GetService<CakesDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            ShoppingCart shoppingcart = new ShoppingCart(context);
            shoppingcart.ShoppingCartId = cartId;

            return shoppingcart;
        }

        public void AddtoCart(Cake cake, int amount)
        {
            var shoppingCartItem = _cakedbcontext.ShoppingCartItems.SingleOrDefault(
                s => s.cake.CakeId == cake.CakeId && s.ShoppingCartId == ShoppingCartId
            );

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    cake = cake,
                    Amount = amount
                };
            }
            _cakedbcontext.ShoppingCartItems.Add(shoppingCartItem);
            _cakedbcontext.SaveChanges();
        }

        public void RemoveCart(int shoppingcartitemid)
        {
            var shoppingCartItem = _cakedbcontext.ShoppingCartItems.SingleOrDefault(s =>
                                      s.ShoppingCartId == ShoppingCartId  && 
                                      s.ShoppingCartItemId == shoppingcartitemid 
              );
            if(shoppingCartItem != null)
            {
                _cakedbcontext.ShoppingCartItems.Remove(shoppingCartItem);
                _cakedbcontext.SaveChanges();
            }
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            var ShoppingCartItems = shoppingCartItems ??  // If it is Null, will pull the items from the database
                                        (
                                           shoppingCartItems = _cakedbcontext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                                           .Include(s => s.cake)  //Because it is in a different table --> referenced by foreign key
                                           .ToList()
                                         );
            return shoppingCartItems;
        }

        public void ClearCart()
        {
            var shoppingCartItems = _cakedbcontext.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId);
            _cakedbcontext.ShoppingCartItems.RemoveRange(shoppingCartItems);
            _cakedbcontext.SaveChanges();
        }

        public decimal shoppingCartTotal()
        {
            decimal total = _cakedbcontext.ShoppingCartItems.Where(s => s.ShoppingCartId == ShoppingCartId)
                                         .Select(c=> c.cake.Price * c.Amount).Sum();
            return total;
        }
    }
}
