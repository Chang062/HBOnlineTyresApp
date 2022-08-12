using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Cart;
using HBOnlineTyresApp.Data.Services;
using HBOnlineTyresApp.Data.Static;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;

namespace HBOnlineTyresApp.Controllers
{
    [Authorize]

    public class OrdersController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public OrdersController(IInventoryService inventoryService, ShoppingCart shoppingCart, IOrdersService ordersService, AppDbContext context, IEmailSender emailSender)
        {
            _inventoryService = inventoryService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
            _context = context;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            ViewBag.delete = TempData["delete"] as string;
           return View(orders.OrderBy(l=> l.Id).ToList());
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
            TempData["msg"] = "Item Was Successfuly Added To Cart.";
            return RedirectToAction("Products", "Inventory");
            

        }
        public async Task<RedirectToActionResult> UpdateShoppingCart(int id)
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

            await _emailSender.SendEmailAsync(userEmailAddress, "Order Completed",
                  $"Your order was successful, kindly pick up your order from our \nwharehouse or contact us at (876) 123-4567 to arrange for delivery.\n\nThank you.");


            return View("OrderCompleted");
           
        }
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Orders.FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityentry = _context.Entry<Order>(entity);
            entityentry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            TempData["delete"] = "Delete Operation Was Successfuly Completed.";
            return RedirectToAction(nameof(Index));
        }

     
    }
}
