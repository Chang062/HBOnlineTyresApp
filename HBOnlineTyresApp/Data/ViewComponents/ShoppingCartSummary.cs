using HBOnlineTyresApp.Data.Cart;
using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HBOnlineTyresApp.Data.ViewComponents
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly UserManager<ApplicationUser> _userManager;
       

        public ShoppingCartSummary(ShoppingCart shoppingCart, UserManager<ApplicationUser> userManager)
        {
            _shoppingCart = shoppingCart;
            _userManager = userManager;
          
        }

        public IViewComponentResult Invoke()
        {
            string userId = _userManager.GetUserId(HttpContext.User);            
            var items = _shoppingCart.GetShoppingCartItems(userId);
            return View(items.Count);
        }
    }
}
