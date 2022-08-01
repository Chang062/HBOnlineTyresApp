using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HBOnlineTyresApp.Data.Messages
{
    public class Inbox
    {
        public AppDbContext _context { get; set; }
        public string ContactUsId { get; set; }

        public List<Message> Messages { get; set; }


        public Inbox(AppDbContext context)
        {
            _context = context;
        }

        public static Inbox GetInbox(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string msgId = session.GetString("msgId") ?? Guid.NewGuid().ToString();
            session.SetString("msgId", msgId);

            return new Inbox(context) { ContactUsId = msgId };
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
            if (msg != null)
            {
                messages.FullName = msg.FullName;
                messages.PhoneNumber = msg.PhoneNumber;
                messages.Description = msg.Description;
                messages.Viewed = true;
                messages.DateReceived = msg.DateReceived;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Message> GetMsgByIdAsync(int id)
        {
            var details = await _context.Messages.FirstOrDefaultAsync(l => l.Id == id);
            return details;
        }


        public List<Message> GetAllUnreadMessages(bool read)
        {
            read = false;
            var list = _context.Messages.Where(q => q.Viewed == read).ToList();
            return list;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            var results = await _context.Messages.ToListAsync();
            return results;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Messages.FirstOrDefaultAsync(n => n.Id == id);
            EntityEntry entityentry = _context.Entry<Message>(entity);
            entityentry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }





    }
}
