using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Data.Services
{
    public class TyresService:EntityBaseRepository<Tyre>, ITyresService
    {
        private readonly AppDbContext _context;

        public TyresService(AppDbContext context): base(context)
        {
            _context = context;
        }

        public async Task<Tyre> GetTyreByIdAsync(int id)
        {
            var details = await _context.Tyres.Include(t=> t.Manufacturer).FirstOrDefaultAsync(n=> n.Id==id);
            return details;
        }
    }
}
