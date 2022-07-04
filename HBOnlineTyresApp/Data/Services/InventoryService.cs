﻿using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;
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

        public async Task<NewInventoryDropdownVM> GetNewInventoryDropdownValues()
        {
            var response = new NewInventoryDropdownVM();
            response.Specifications = await _context.Specifications.OrderBy(n=> n.Tyre.Name).ToListAsync();

            return response;
        }
    }
}
