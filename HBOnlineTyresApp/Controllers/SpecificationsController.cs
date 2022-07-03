using HBOnlineTyresApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Controllers
{
    public class SpecificationsController : Controller
    {
        private readonly AppDbContext _AppContext;

        public SpecificationsController(AppDbContext context)
        {
            _AppContext = context;

        }
        public async Task<IActionResult> Index()
        {
            var data = await _AppContext.Specifications.Include("Tyre").ToListAsync();
            return View(data);
        }
    }
}
