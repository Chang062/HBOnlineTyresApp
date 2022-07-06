using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Controllers
{
    public class TyresController : Controller
    {
        private readonly ITyresService _service;

        public TyresController(ITyresService service)
        {
           _service = service;

        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAsync(n => n.Manufacturer, c => c.category);
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var tyreDropdownData = await _service.GetNewTyreDropdownValues();
            ViewBag.CategoryId = new SelectList(tyreDropdownData.category, "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(tyreDropdownData.manufacturers, "Id", "Name");
            return View();
        }
    }
}
