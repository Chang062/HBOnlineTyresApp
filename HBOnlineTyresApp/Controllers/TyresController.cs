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
            ViewBag.msg = TempData["msg"] as string;
            ViewBag.delete = TempData["delete"] as string;
            ViewBag.edit = TempData["edit"] as string;
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var tyreDropdownData = await _service.GetNewTyreDropdownValues();
            ViewBag.CategoryId = new SelectList(tyreDropdownData.category, "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(tyreDropdownData.manufacturers, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewTyreVM tyre)
        {
            if (!ModelState.IsValid)
            {
                var tyreDropdownData = await _service.GetNewTyreDropdownValues();
                ViewBag.CategoryId = new SelectList(tyreDropdownData.category, "Id", "Name");
                ViewBag.ManufacturerId = new SelectList(tyreDropdownData.manufacturers, "Id", "Name");
                return View();
            }
            await _service.AddNewTyreAsync(tyre);
            TempData["msg"] = "Record was successfully Created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tyreDetails = await _service.GetTyreByIdAsync(id);
            if (tyreDetails == null) return View("NotFound");

            var response = new NewTyreVM() { 
                Id = tyreDetails.Id,
                Name = tyreDetails.Name,
                ImageURL = tyreDetails.ImageURL,
                CategoryId = tyreDetails.CategoryId,
                ManufacturerId = tyreDetails.ManufacturerId,
                Description = tyreDetails.Description,
                
            };

            var tyreDropdownData = await _service.GetNewTyreDropdownValues();
            ViewBag.CategoryId = new SelectList(tyreDropdownData.category, "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(tyreDropdownData.manufacturers, "Id", "Name");
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id, NewTyreVM tyre)
        {
            if (id != tyre.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var tyreDropdownData = await _service.GetNewTyreDropdownValues();
                ViewBag.CategoryId = new SelectList(tyreDropdownData.category, "Id", "Name");
                ViewBag.ManufacturerId = new SelectList(tyreDropdownData.manufacturers, "Id", "Name");
                return View();
            }
            await _service.UpdateTyreAsync(tyre);
            TempData["edit"] = "Record Was Successfuly Updated.";
            return RedirectToAction(nameof(Index));
        }
   
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.GetIdAync(id);
            if (result == null) return View("NotFound");

            await _service.DeleteAsync(id);
            TempData["delete"] = "Delete Operation Was Successfuly Completed.";
            return RedirectToAction(nameof(Index));
        }

    }
}
