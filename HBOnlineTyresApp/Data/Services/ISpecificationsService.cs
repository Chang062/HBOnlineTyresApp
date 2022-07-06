using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HBOnlineTyresApp.Data.Services
{
    public interface ISpecificationsService:IEntityBaseRepository<Specification>
    {
        Task<Specification> GetSpecificationsByIdAsync(int id);
        Task<SelectList> GetDropdownValues();
    }
}
