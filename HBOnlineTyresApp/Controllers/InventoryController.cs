using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Services;
using HBOnlineTyresApp.Data.ViewModels;
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
            return View(data.OrderBy(l=> l.Specifications.Tyre.Name));
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

        [HttpPost]
        public async Task<IActionResult> Create(NewInventoryVM inventory)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.SpecsId = await _service.GetNewInventoryDropdownValues();  //new SelectList(dropdownData.Specs, "Id", "Name");
                return View();
            }

            await _service.AddNewInventoryAsync(inventory);
            return RedirectToAction(nameof(Index));
        }

        //Update
        public async Task<IActionResult> Edit(int id)
        {
            var inventoryDetails = await _service.GetInventoryByIdAsync(id);
            if (inventoryDetails == null) return View("NotFound");

            var response = new NewInventoryVM()
            {
                Id = inventoryDetails.Id,
                SpecsId = inventoryDetails.SpecsId,
                Quantity = inventoryDetails.Quantity,
            };
            
            ViewBag.SpecsId = await _service.GetNewInventoryDropdownValues();  //new SelectList(dropdownData.Specs, "Id", "Name");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewInventoryVM inventory)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SpecsId = await _service.GetNewInventoryDropdownValues();  //new SelectList(dropdownData.Specs, "Id", "Name");
                return View();
            }

            await _service.UpdateInventoryAsync(inventory);
            return RedirectToAction(nameof(Index));
        }
        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            var inventoryDetails = await _service.GetInventoryByIdAsync(id);
            if (inventoryDetails == null) return View("NotFound");

            var response = new NewInventoryVM()
            {
                Id = inventoryDetails.Id,
                SpecsId = inventoryDetails.SpecsId,
                Quantity = inventoryDetails.Quantity,
            };

            ViewBag.SpecsId = await _service.GetNewInventoryDropdownValues();  //new SelectList(dropdownData.Specs, "Id", "Name");
            return View(response);
        }

        [HttpPost, ActionName ("Delete")]
        public async Task<IActionResult> Remove(int id, NewInventoryVM inventory)
        {
            var inventoryDetails = await _service.GetInventoryByIdAsync(id);
            if (inventoryDetails == null) return View("NotFound");
            
            if (!ModelState.IsValid)
            {
                ViewBag.SpecsId = await _service.GetNewInventoryDropdownValues();  //new SelectList(dropdownData.Specs, "Id", "Name");
                return View();
            }

            await _service.DeleteInventoryAsync(inventory);
            return RedirectToAction(nameof(Index));
        }
    }
}
