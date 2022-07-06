using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HBOnlineTyresApp.Data.Services
{
    public class TyresService:EntityBaseRepository<Tyre>, ITyresService
    {
        private readonly AppDbContext _context;

        public TyresService(AppDbContext context): base(context)
        {
            _context = context;
        }

        public async Task AddNewTyreAsync(NewTyreVM data)
        {
            var newTyre = new Tyre()
            {
                Name = data.Name,
                CategoryId = data.CategoryId,
                ImageURL = data.ImageURL,
                ManufacturerId = data.ManufacturerId,
                Description = data.Description,
            };
            _context.Tyres.AddAsync(newTyre);
            await _context.SaveChangesAsync();
        }

        public async Task<NewTyreDropdownVM> GetNewTyreDropdownValues()
        {
            var response = new NewTyreDropdownVM();

            response.category = await _context.Categories.OrderBy(q => q.Name).ToListAsync();
            response.manufacturers = await _context.Manufacturers.OrderBy(q => q.Name).ToListAsync();
            
            
            return response;
        }

        public async Task<Tyre> GetTyreByIdAsync(int id)
        {
            var details = await _context.Tyres.Include(t=> t.Manufacturer).FirstOrDefaultAsync(n=> n.Id==id);
            return details;
        }

        public async Task UpdateTyreAsync(NewTyreVM data)
        {
            var dbTyre = await _context.Tyres.FirstOrDefaultAsync(q => q.Id == data.Id);
            if (dbTyre != null)
            {

                dbTyre.Name = data.Name;
                dbTyre.CategoryId = data.CategoryId;
                dbTyre.ImageURL = data.ImageURL;
                dbTyre.ManufacturerId = data.ManufacturerId;
                dbTyre.Description = data.Description;
                   
                await _context.SaveChangesAsync();
            }
    

        }
        public async Task DeleteTyreAsync(NewTyreVM data)
        {
            var dbTyre = await _context.Tyres.FirstOrDefaultAsync(q => q.Id == data.Id);
            if (dbTyre != null)
            {

                dbTyre.Name = data.Name;
                dbTyre.CategoryId = data.CategoryId;
                dbTyre.ImageURL = data.ImageURL;
                dbTyre.ManufacturerId = data.ManufacturerId;
                dbTyre.Description = data.Description;

              
             
               EntityEntry entityentry = _context.Entry<Tyre>(dbTyre);
               entityentry.State = EntityState.Deleted;
              // _context.Tyres.Remove(dbTyre);
                await _context.SaveChangesAsync();

            }


        }
    }
}
