using HBOnlineTyresApp.Data.Base;
using HBOnlineTyresApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HBOnlineTyresApp.Data.Services
{
    public class ManufacturersService :EntityBaseRepository<Manufacturer>, IManufacturersService
    {
         public ManufacturersService(AppDbContext context): base(context) { }
  
    }
}
