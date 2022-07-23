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
            return RedirectToAction(nameof(Index));
            
        }
        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            var Details = await _service.GetIdAync(id);
            if (Details == null) return View("NotFound");
            return View(Details);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _service.GetIdAync(id);
            if (result == null) return View("NotFound");

            await _service.DeleteAsync(id);
            
          return RedirectToAction(nameof(Index));

        }
    }
}
