using HBOnlineTyresApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ShoppingCartId { get; set; }
        public string Message { get; set; } = string.Empty;

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

        public void AddItemToCart(Inventory inventory)
        {
            var cartItem = _context.ShoppingCartItems.FirstOrDefault(q=> q.Inventory.Id == inventory.Id
            && q.ShoppingCartId == ShoppingCartId);

            if(cartItem == null)
            {
                cartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Inventory = inventory,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(cartItem);
            }
            else if( cartItem.Amount >= cartItem.Inventory.Quantity)
            {
                Message = "The remaining item is already added";
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
            && q.ShoppingCartId == ShoppingCartId);

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
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShopingCartItems ?? (ShopingCartItems = _context.ShoppingCartItems.Where(q => q.ShoppingCartId == ShoppingCartId)
                .Include(q => q.Inventory.Specifications.Tyre).ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(q=> q.ShoppingCartId == ShoppingCartId)
                .Select(q=> q.Inventory.Specifications.Cost * q.Amount + (q.Inventory.Specifications.Cost * 0.15)).Sum();
            return total;
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(q => q.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
