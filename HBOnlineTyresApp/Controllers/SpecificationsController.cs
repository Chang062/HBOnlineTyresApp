using HBOnlineTyresApp.Data;
using HBOnlineTyresApp.Data.Services;
using HBOnlineTyresApp.Data.Static;
using HBOnlineTyresApp.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
   
    public class SpecificationsController : Controller
    {
        private readonly ISpecificationsService _service;

        public SpecificationsController(ISpecificationsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAsync(t=> t.Tyre, c=> c.Tyre.category, m=> m.Tyre.Manufacturer);
            ViewBag.msg = TempData["msg"] as string;
            ViewBag.delete = TempData["delete"] as string;
            ViewBag.edit = TempData["edit"] as string;
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.TyreId = await _service.GetDropdownValues();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewSpecificationVM specs)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.TyreId = await _service.GetDropdownValues();
                return View();
            }
            await _service.AddNewSpecificationAsync(specs);
            TempData["msg"] = "Record was successfully Created.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var specsDetails = await _service.GetSpecificationsByIdAsync(id);
            if (specsDetails == null) return View("NotFound");

            var response = new NewSpecificationVM()
            {
                Id = specsDetails.Id,
                TyreId = specsDetails.TyreId,
                Size = specsDetails.Size,
                RimSize = specsDetails.RimSize,
                ServiceDescription = specsDetails.ServiceDescription,
                SideWall = specsDetails.SideWall,
                Diameter = specsDetails.Diameter,
                MaxPSI = specsDetails.MaxPSI,
                SectionWidth = specsDetails.SectionWidth,
                MaxLoad = specsDetails.MaxLoad,
                Weight = specsDetails.Weight,
                ThreadDept = specsDetails.ThreadDept,
                AprovedRimWidth = specsDetails.AprovedRimWidth,
                Cost = specsDetails.Cost,
            };


            ViewBag.TyreId = await _service.GetDropdownValues();

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewSpecificationVM specs)
        {
            var specsDetails = await _service.GetSpecificationsByIdAsync(id);
            if (specsDetails == null) return View("NotFound");
            if (!ModelState.IsValid)
            {
                ViewBag.TyreId = await _service.GetDropdownValues();
                return View();
            }
            await _service.UpdateSpecificationAsync(specs);
            TempData["edit"] = "Record Was Successfuly Updated.";
            return RedirectToAction(nameof(Index));
        }

    
        public async Task<IActionResult> Delete(int id)
        {
            var specsDetails = await _service.GetSpecificationsByIdAsync(id);
            if (specsDetails == null) return View("NotFound");
           
            await _service.DeleteAsync(id);
            TempData["delete"] = "Delete Operation Was Successfuly Completed.";
            return RedirectToAction(nameof(Index));
        }
    }
}
