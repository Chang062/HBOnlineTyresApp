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

        [TempData]
        public string StatusMessage { get; set; }
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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items = _shoppingCart.GetShoppingCartItems(userId);
            
            _shoppingCart.ShopingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(userId),
                ShoppingSubTotal = _shoppingCart.GetShoppingSubTotal(userId),
                ShoppingTaxTotal = _shoppingCart.GetShoppingTaxTotal(userId),

            };
            return View(response);
        }
        public async Task <RedirectToActionResult> AddToShoppingCart(int id)
        {
            var item = await _inventoryService.GetInventoryByIdAsync(id);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item, userId);
                
                
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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var items =  _shoppingCart.GetShoppingCartItems(userId);            
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
           await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);

            await _shoppingCart.ClearShoppingCartAsync(userId);

            return View("OrderCompleted");

        }
    }
}
