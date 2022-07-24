using HBOnlineTyresApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

      

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(l=> l.OrderItems).ThenInclude(l=> l.Inventory.Specifications.Tyre.Manufacturer).Include(l=> l.User)
                .ToListAsync();
            if(userRole != "Administrator")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
                OrderDate = DateTime.Now,

            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    InventoryId = item.Inventory.Id,
                    OrderId = order.Id,
                    Price = item.Inventory.Specifications.Cost
                };
                await _context.OrderItems.AddAsync(orderItem);

                item.Inventory.Quantity -= item.Amount;
                ////var inventoryRecord = await _context.Inventories.AsNoTracking().FirstOrDefaultAsync(q => q.Id == item.Inventory.Id);
                ////inventoryRecord.Quantity -= item.Amount;
                ////_context.Update(inventoryRecord);
            }
            await _context.SaveChangesAsync();
        }
    }
}
