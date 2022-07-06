using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Controllers
{
    public class SpecificationsController : Controller
    {
        private readonly ISpecificationsService _service;

        public SpecificationsController(ISpecificationsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAsync(t=> t.Tyre, c=> c.Tyre.category, m=> m.Tyre.Manufacturer);
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.TyreId = await _service.GetDropdownValues();
            return View();
        }
    }
}
