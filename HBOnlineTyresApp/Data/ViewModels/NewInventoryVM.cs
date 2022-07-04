using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class NewInventoryVM
    {
        [Required (ErrorMessage = "This field cannot be left empty")]
        [Display(Name ="Specification Identification Number")]
        public int SpecsId { get; set; }
        [Required(ErrorMessage = "This field cannot be left empty")]
        [Display(Name = "In Stock ")]
        
        public int Quantity { get; set; }
    }
}

