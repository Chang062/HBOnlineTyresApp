using HBOnlineTyresApp.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace HBOnlineTyresApp.Data.ViewComponents
{
    public class MessagesVC:ViewComponent
    {
        private readonly ContactUsService _message;

        public MessagesVC(ContactUsService message)
        {
            _message = message;
        }


        public IViewComponentResult Invoke()
        {
            var items = _message.UnreadMessages();
            return View(items.Count);
        }
    }
}
