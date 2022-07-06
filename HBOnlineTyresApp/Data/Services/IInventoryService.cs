using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HBOnlineTyresApp.Data.Services
{
    public interface IInventoryService:IEntityBaseRepository<Inventory>
    {
        Task<Inventory> GetInventoryByIdAsync(int id);
         Task<SelectList> GetNewInventoryDropdownValues();

    }
}
