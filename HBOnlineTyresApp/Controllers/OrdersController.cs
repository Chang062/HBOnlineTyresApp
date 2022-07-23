using HBOnlineTyresApp.Data.Cart;
using HBOnlineTyresApp.Data.Services;
using HBOnlineTyresApp.Data.Static;
using HBOnlineTyresApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HBOnlineTyresApp.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IInventoryService inventoryService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _inventoryService = inventoryService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
            
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {

            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShopingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
                ShoppingSubTotal = _shoppingCart.GetShoppingSubTotal(),
                ShoppingTaxTotal = _shoppingCart.GetShoppingTaxTotal(),

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

        public async Task <IActionResult> CompleteOrder()
        {
            var items =  _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
           await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);

            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");

        }
    }
}
