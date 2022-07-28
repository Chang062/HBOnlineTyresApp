using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Data.Services
{
    public class ContactUsService :EntityBaseRepository<Message>,  IContactUsService
    {
        AppDbContext _context;

       public List<Message> Messages { get; set; }
        public ContactUsService(AppDbContext context) : base(context) 
        {
            
            _context = context;
        }

        public Task DeleteMsgAsync(MessageVM msg)
         {
             throw new NotImplementedException();
         }

         public async Task PostNewAsync(MessageVM msg)
         {
             var newMsg = new Message()
             {
                 FullName = msg.FullName,
                 PhoneNumber = msg.PhoneNumber,
                 Description = msg.Description,
                 Viewed = false,
                 DateReceived = DateTime.Now,
             };
             await _context.AddAsync(newMsg);
             await _context.SaveChangesAsync();
         }

         public async Task UpdateAsync(MessageVM msg)
         {
            var messages = await _context.Messages.FirstOrDefaultAsync(l => l.Id == msg.Id);
            if(msg != null)
            {
                messages.FullName = msg.FullName;
                messages.PhoneNumber = msg.PhoneNumber;
                messages.Description = msg.Description;
                messages.Viewed = msg.Viewed;
                messages.DateReceived = msg.DateReceived;

               await _context.SaveChangesAsync();
            }
         }

         public async Task<Message> GetMsgByIdAsync(int id)
         {
            var details = await _context.Messages.FirstOrDefaultAsync(l => l.Id == id);
            return details;
         }

        public List<Message> UnreadMessages()
        {
           var result = _context.Messages.Where(l=> l.Viewed == true).ToList();
            return result;
        }
    }
}
