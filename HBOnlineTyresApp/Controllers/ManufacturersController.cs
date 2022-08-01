using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Services;
using HBOnlineTyresApp.Data.Static;
using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    public class ManufacturersController : Controller
    {
        private readonly IManufacturersService _service;

        public ManufacturersController(IManufacturersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            ViewBag.msg = TempData["msg"] as string;
            ViewBag.delete = TempData["delete"] as string;
            ViewBag.edit = TempData["edit"] as string;
            return View(data);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, LogoURL, Description")]Manufacturer manufacturer)
        {
            if(!ModelState.IsValid)
            {
                return View(manufacturer);
            }
            await _service.AddAsync(manufacturer);
            TempData["msg"] = "Record was successfully Created.";
            return RedirectToAction(nameof(Index));
        }

        //Update
        public async Task <IActionResult> Edit(int id)
        {
            var UpdateDetails = await _service.GetIdAync(id);
            if (UpdateDetails == null) return View("NotFound");
            return View(UpdateDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, LogoURL, Description")] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return View(manufacturer);
            }
            await _service.UpdateAsync(id, manufacturer);
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
