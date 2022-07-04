using HBOnlineTyresApp.Models;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class NewInventoryDropdownVM
    {
        public NewInventoryDropdownVM()
        {
            Specifications = new List<Specification>();
        }
        public List <Specification> Specifications{ get; set; }
    }
}
