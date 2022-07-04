using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Models;

namespace HBOnlineTyresApp.Data.Services
{
    public interface ISpecificationsService:IEntityBaseRepository<Specification>
    {
        Task<Specification> GetSpecificationsByIdAsync(int id);
    }
}
