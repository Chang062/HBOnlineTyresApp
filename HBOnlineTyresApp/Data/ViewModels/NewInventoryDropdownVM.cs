using HBOnlineTyresApp.Models;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class NewInventoryDropdownVM
    {
        public NewInventoryDropdownVM()
        {
            Specs = new List<Specification>();
          
        }
        public List <Specification> Specs{ get; set; }
        
    }
}
