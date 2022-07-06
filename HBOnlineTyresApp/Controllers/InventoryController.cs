using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        //Filter
        public async Task<IActionResult> Filter(string searchString)

        {

            var data = await _service.GetAsync(n => n.Specifications.Tyre.Manufacturer, p => p.Specifications.Tyre.category);

            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResults = data.Where(l=> l.Specifications.Tyre.Name.Contains(searchString) 
                || l.Specifications.Tyre.Manufacturer.Name.Contains(searchString) || l.Specifications.Tyre.category.Name.Contains(searchString)
                || l.Specifications.Size.Contains(searchString)).ToList();
                return View("Products", filteredResults);
            }
            return View("Products", data);
        }

        //show product details

        public async Task <IActionResult> Details (int id)
        {
            var productDetails = await _service.GetInventoryByIdAsync(id);
            return View(productDetails);
        }

        //CREATE
        public async Task< IActionResult> Create()
        {
            ViewBag.SpecsId = await _service.GetNewInventoryDropdownValues();  //new SelectList(dropdownData.Specs, "Id", "Name");
            return View();
        }
    }
}
