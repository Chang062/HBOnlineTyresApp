using HBOnlineTyresApp.Data.Messages;
using HBOnlineTyresApp.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace HBOnlineTyresApp.Data.ViewComponents
{
    
    public class UnreadMessages: ViewComponent
    {
        private readonly Inbox _service;

        public UnreadMessages(Inbox service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke()
        {
            bool read = false;
            var messages = _service.GetAllUnreadMessages(read);
            return View(messages.Count);
        }

    }
}
