using HBOnlineTyresApp.Data.Services;
using HBOnlineTyresApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HBOnlineTyresApp.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IContactUsService _service;

        public ContactUsController(IContactUsService service)
        {
            _service = service;

        }
        public IActionResult Index()
        {
            ViewBag.msg = TempData["msg"] as string;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MessageVM contact)
        {
            if (ModelState.IsValid)
            {
                await _service.PostNewAsync(contact);

                TempData["msg"] = "Message was successfully sent";

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Messages()
        {
            var messages = await _service.GetAllAsync();
            return View(messages);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var details = await _service.GetMsgByIdAsync(id);
            if (details == null) return View("NotFound");

            var response = new MessageVM()
            {
                Id = details.Id,
                FullName = details.FullName,
                PhoneNumber = details.PhoneNumber,
                DateReceived = details.DateReceived,
                Description = details.Description,
                Viewed = details.Viewed,
            };
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, MessageVM msgVm)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(msgVm);
                return RedirectToAction(nameof(Messages));
            }

            return View();
        }
    }
}
