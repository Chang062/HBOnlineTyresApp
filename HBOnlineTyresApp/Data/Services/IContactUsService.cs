using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;

namespace HBOnlineTyresApp.Data.Services
{
    public interface IContactUsService: IEntityBaseRepository<Message>
    {

        Task PostNewAsync(MessageVM msg);
        Task UpdateAsync(MessageVM msg);
        Task DeleteMsgAsync(MessageVM msg);
        Task<Message> GetMsgByIdAsync(int id);
    }
}
