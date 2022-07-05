using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Data.Services
{
    public class InventoryService:EntityBaseRepository<Inventory>, IInventoryService
    {
        private readonly AppDbContext _context;
        public InventoryService(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<Inventory> GetInventoryByIdAsync(int id)
        {
            var productDetails = await _context.Inventories
                .Include(s => s.Specifications)
                .Include(t => t.Specifications.Tyre)
                .Include(c => c.Specifications.Tyre.category)
                .Include(m => m.Specifications.Tyre.Manufacturer).FirstOrDefaultAsync(n => n.Id == id);

            return productDetails;
                
        }

        /* public async Task<NewInventoryDropdownVM> GetNewInventoryDropdownValues()
         {
             var response = new NewInventoryDropdownVM();

             // response.Specs = await _context.Specifications.Include(t=> t.Tyre).Select(q=> new {Id= q.Id, Size= $"{q.Tyre.Name} {q.Size}"}).ToListAsync();
             //response.Specs = await _context.Specifications.ToListAsync();
             response.Specs = await _context.Specifications.Include(q=> q.Tyre).ToListAsync();

             return response;
         }*/
        public async Task<SelectList> GetNewInventoryDropdownValues()
        {
          
            var specs = await _context.Specifications.Include(t => t.Tyre).Select(q => new { Id = q.Id, Name = $"{q.Tyre.Manufacturer.Name}|{q.Tyre.Name }|{q.Size}" }).ToListAsync();
            return new SelectList(specs, "Id", "Name");
        }


    }
}
