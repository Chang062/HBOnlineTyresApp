using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HBOnlineTyresApp.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }

    

        public List<ShoppingCartItem> ShopingCartItems {get; set;}


        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string cartId = session.GetString("cartId")?? Guid.NewGuid().ToString();
            session.SetString("cartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Inventory inventory, string userId)
        {

            var cartItem = _context.ShoppingCartItems.FirstOrDefault(q=> q.Inventory.Id == inventory.Id
            && q.UserId == userId);

            if(cartItem == null)
            {
                cartItem = new ShoppingCartItem()
                {
                   UserId = userId,
                    Inventory = inventory,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(cartItem);
            }
            else if( cartItem.Amount >= cartItem.Inventory.Quantity)
            {

                
            }
               
            else
            {
                cartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Inventory inventory)
        {
            var cartItem = _context.ShoppingCartItems.FirstOrDefault(q => q.Inventory.Id == inventory.Id
            /*&& q.ShoppingCartId == ShoppingCartId*/);

            if (cartItem != null)
            {
                if(cartItem.Amount > 1)
                {
                    cartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(cartItem);
                }              
                
            }
                _context.SaveChanges();
        }
        public List<ShoppingCartItem> GetShoppingCartItems(string userId)
        {
            //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ShopingCartItems = _context.ShoppingCartItems.Where(l=> l.UserId == userId)
                .Include(q => q.Inventory.Specifications.Tyre).ThenInclude(l=> l.Manufacturer).ToList();

            return ShopingCartItems;

        }
        

        public double GetShoppingCartTotal(string userId)
        {
            var total = _context.ShoppingCartItems.Where(q=> q.UserId == userId)
                .Select(q=> q.Inventory.Specifications.Cost * q.Amount + (q.Inventory.Specifications.Cost * 0.15)).Sum();
            return total;
        }

        public double GetShoppingSubTotal(string userId)
        {
            var total = _context.ShoppingCartItems.Where(q => q.UserId == userId)
                .Select(q => q.Inventory.Specifications.Cost * q.Amount).Sum();
            return total;
        }
        public double GetShoppingTaxTotal(string userId)
        {
            var total = _context.ShoppingCartItems.Where(q => q.UserId == userId)
                .Select(q => q.Inventory.Specifications.Cost * q.Amount*(.15)).Sum();
            return total;
        }



        public async Task ClearShoppingCartAsync(string userId)
        {
            var items = await _context.ShoppingCartItems.Where(q => q.UserId == userId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
