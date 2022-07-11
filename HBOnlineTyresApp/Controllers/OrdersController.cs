using HBOnlineTyresApp.Data.Cart;
using HBOnlineTyresApp.Data.Services;
using HBOnlineTyresApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HBOnlineTyresApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly ShoppingCart _shoppingCart;
        public OrdersController(IInventoryService inventoryService, ShoppingCart shoppingCart)
        {
            _inventoryService = inventoryService;
            _shoppingCart = shoppingCart;
        }
        public IActionResult ShoppingCart()
        {

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShopingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),

            };
            return View(response);
        }
        public async Task <RedirectToActionResult> AddToShoppingCart(int id)
        {
            var item = await _inventoryService.GetInventoryByIdAsync(id);
            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));

        }

        public async Task<RedirectToActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _inventoryService.GetInventoryByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));

        }
    }
}
