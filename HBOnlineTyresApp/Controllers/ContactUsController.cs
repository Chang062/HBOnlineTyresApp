using HBOnlineTyresApp.Data.Messages;
using HBOnlineTyresApp.Data.Services;
using HBOnlineTyresApp.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HBOnlineTyresApp.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly Inbox _service;
        

        public ContactUsController(Inbox service)
        {
            _service = service;

        }
        public IActionResult Index()
        {
            ViewBag.msg = TempData["msg"] as string;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            bool read = false;
            var messages = await _service.GetAllAsync();
            var unread =  _service.GetAllUnreadMessages(read);
            ViewBag.del = TempData["del"] as string;
            return View(messages.OrderBy(l=> l.Viewed==false));

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MessageVM msgVm)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(msgVm);
                return RedirectToAction(nameof(Messages));
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var inbox = await _service.GetMsgByIdAsync(id);
            if (inbox == null) return View("NotFound");
            await _service.DeleteAsync(id);
            TempData["del"] = "Delete Operation Was Successfuly Completed.";
            return RedirectToAction(nameof(Messages));
        }

    }
}
