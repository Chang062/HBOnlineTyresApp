using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryService _service;
       
        public InventoryController(IInventoryService service)
        {
            _service = service;
        }
       
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAsync(n => n.Specifications.Tyre.Manufacturer);
            return View(data);
        }
        public async Task<IActionResult> Products()

        {
                     
            var data = await _service.GetAsync(n=> n.Specifications.Tyre.Manufacturer, p=> p.Specifications.Tyre.category);
            return View(data);
        }

        //show product details

        public async Task <IActionResult> Details (int id)
        {
            var productDetails = await _service.GetInventoryByIdAsync(id);
            return View(productDetails);
        }

        //CREATE
        public IActionResult Create()
        {
            return View();
        }
    }
}
