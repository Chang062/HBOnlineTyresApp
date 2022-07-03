using HBOnlineTyresApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Controllers
{
    public class TyresController : Controller
    {
        private readonly AppDbContext _Appcontext;

        public TyresController(AppDbContext context)
        {
            _Appcontext = context;

        }
        public async Task<IActionResult> Index()
        {
            var data = await _Appcontext.Tyres.Include("Manufacturer").ToListAsync();
            return View(data);
        }
    }
}
