using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Models;

namespace HBOnlineTyresApp.Data.Services
{
    public interface IInventoryService:IEntityBaseRepository<Inventory>
    {
        Task<Inventory> GetInventoryByIdAsync(int id);
    }
}
