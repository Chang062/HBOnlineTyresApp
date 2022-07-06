using HBOnlineTyresApp.Models;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class NewTyreDropdownVM
    {
        public NewTyreDropdownVM()
        {
            category = new List<Category>();
            manufacturers = new List<Manufacturer>();
        }
        public List<Category> category { get; set; }
        public List<Manufacturer> manufacturers { get; set; }
    }
}