using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Models;
using Microsoft.EntityFrameworkCore;
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
        public  async Task<Specification> GetSpecificationsByIdAsync(int id)
        {
           var details = _context.Specifications.Include(t => t.Tyre)
                .Include(m => m.Tyre.Manufacturer)
                .Include(c => c.Tyre.category).FirstOrDefault(n => n.Id == id);
            return details;

      
        }
    }
}
