using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Models;

namespace HBOnlineTyresApp.Data.Services
{
    public interface ITyresService:IEntityBaseRepository<Tyre>
    {
        Task<Tyre> GetTyreByIdAsync(int id);
    }
}
