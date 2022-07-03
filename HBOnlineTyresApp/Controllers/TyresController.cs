using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Services;
using Microsoft.AspNetCore.Mvc;
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
            var data = await _service.GetAsync(n => n.Manufacturer);
            return View(data);
        }
    }
}
