using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Data.ViewModels;
using HBOnlineTyresApp.Models;

namespace HBOnlineTyresApp.Data.Services
{
    public interface ITyresService:IEntityBaseRepository<Tyre>
    {
        Task<Tyre> GetTyreByIdAsync(int id);
        Task<NewTyreDropdownVM> GetNewTyreDropdownValues();
    }
}
