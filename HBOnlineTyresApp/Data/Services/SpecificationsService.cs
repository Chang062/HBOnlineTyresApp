using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace HBOnlineTyresApp.Data.Services
{
    public class SpecificationsService : EntityBaseRepository<Specification>, ISpecificationsService
    {
        private readonly AppDbContext _context;
        public SpecificationsService(AppDbContext context): base(context)
        {
            _context = context;
        }

        //Add Specification
        public async Task AddNewSpecificationAsync(NewSpecificationVM specs)
        {
            var newSpec = new Specification()
            {
                TyreId = specs.TyreId,
                Size = specs.Size,
                RimSize = specs.RimSize,
                ServiceDescription = specs.ServiceDescription,
                SideWall = specs.SideWall,
                Diameter = specs.Diameter,
                MaxPSI = specs.MaxPSI,
                SectionWidth = specs.SectionWidth,
                MaxLoad = specs.MaxLoad,
                Weight = specs.Weight,
                ThreadDept = specs.ThreadDept,
                AprovedRimWidth = specs.AprovedRimWidth,
                Cost = specs.Cost,

            };

            _context.Specifications.AddAsync(newSpec);
            await _context.SaveChangesAsync();
        }

        
        //Dropdown List
        public async Task<SelectList> GetDropdownValues()
        {
            var dropdownList = await _context.Tyres.OrderBy(x => x.Name).ToListAsync();
            return new SelectList(dropdownList, "Id", "Name");
        }

        //Get by ID
        public  async Task<Specification> GetSpecificationsByIdAsync(int id)
        {
           var details = _context.Specifications.Include(t => t.Tyre)
                .Include(m => m.Tyre.Manufacturer)
                .Include(c => c.Tyre.category).FirstOrDefault(n => n.Id == id);
            return   details;

      
        }

        //Update
        public async Task UpdateSpecificationAsync(NewSpecificationVM specs)
        {
            var dbSpecs = await _context.Specifications.FirstOrDefaultAsync(q => q.Id == specs.Id);
            if (dbSpecs != null)
            {
                dbSpecs.TyreId = specs.TyreId;
                dbSpecs.Size = specs.Size;
                dbSpecs.RimSize = specs.RimSize;
                dbSpecs.ServiceDescription = specs.ServiceDescription;
                dbSpecs.SideWall = specs.SideWall;
                dbSpecs.Diameter = specs.Diameter;
                dbSpecs.MaxPSI = specs.MaxPSI;
                dbSpecs.SectionWidth = specs.SectionWidth;
                dbSpecs.MaxLoad = specs.MaxLoad;
                dbSpecs.Weight = specs.Weight;
                dbSpecs.ThreadDept = specs.ThreadDept;
                dbSpecs.AprovedRimWidth = specs.AprovedRimWidth;
                dbSpecs.Cost = specs.Cost;

                await _context.SaveChangesAsync();

            }
        }
    }
}
