using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Services;
using HBOnlineTyresApp.Data.Static;
using HBOnlineTyresApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
  
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
            ViewBag.msg = TempData["msg"] as string;
            ViewBag.del = TempData["del"] as string;
            ViewBag.edit = TempData["edit"] as string;
            ViewBag.error = TempData["error"] as string;

            return View(data);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Products()

        {
                     
            var data = await _service.GetAsync(n=> n.Specifications.Tyre.Manufacturer, p=> p.Specifications.Tyre.category);
            ViewBag.msg = TempData["msg"] as string;
            return View(data.OrderBy(l=> l.Specifications.Tyre.Name));
        }
        //Filter
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)

        {

            var data = await _service.GetAsync(n => n.Specifications.Tyre.Manufacturer, p => p.Specifications.Tyre.category);

            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredResults = data.Where(l=> l.Specifications.Tyre.Name.Contains(searchString) 
                || string.Equals(l.Specifications.Tyre.Manufacturer.Name,searchString, StringComparison.CurrentCultureIgnoreCase) || l.Specifications.Tyre.category.Name.ToLower().Contains(searchString.ToLower())
                || l.Specifications.Size.Contains(searchString)).ToList();
                return View("Products", filteredResults);
            }
            return View("Products", data);
        }

        //show product details
        [AllowAnonymous]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewInventoryVM inventory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.SpecsId = await _service.GetNewInventoryDropdownValues();  //new SelectList(dropdownData.Specs, "Id", "Name");
                    return View();
                }

                await _service.AddNewInventoryAsync(inventory);

                TempData["msg"] = "Item Was Successfuly Added To Inventory.";

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                TempData["error"] = "Something Went Wrong";
                return RedirectToAction(nameof(Index));
            }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,NewInventoryVM inventory)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SpecsId = await _service.GetNewInventoryDropdownValues();  //new SelectList(dropdownData.Specs, "Id", "Name");
                return View();
            }

            await _service.UpdateInventoryAsync(inventory);
            TempData["edit"] = "Item Was Successfuly Updated.";
            return RedirectToAction(nameof(Index));
        }
        //Delete
    
        public async Task<IActionResult> Delete(int id)
        {
            var inventoryDetails = await _service.GetInventoryByIdAsync(id);
            if (inventoryDetails == null) return View("NotFound");
            await _service.DeleteAsync(id);
            TempData["del"] = "Delete Operation Was Successfuly Completed.";
            return RedirectToAction(nameof(Index));
        }
    }
}
