using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Services;
using HBOnlineTyresApp.Data.ViewModels;
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

        [HttpPost]
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
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tyreDetails = await _service.GetTyreByIdAsync(id);
            if (tyreDetails == null) return View("NotFound");

            var response = new NewTyreVM()
            {
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Remove(int id, NewTyreVM tyre)
        {
            if (id != tyre.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var tyreDropdownData = await _service.GetNewTyreDropdownValues();
                ViewBag.CategoryId = new SelectList(tyreDropdownData.category, "Id", "Name");
                ViewBag.ManufacturerId = new SelectList(tyreDropdownData.manufacturers, "Id", "Name");
                return View();
            }
            await _service.DeleteTyreAsync(tyre);
            return RedirectToAction(nameof(Index));
        }

    }
}
