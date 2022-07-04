using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class NewInventoryVM
    {
        [Required (ErrorMessage = "this field cannot be left empty")]
        [Display(Description ="Model")]
        public int SpecsId { get; set; }
        [Required(ErrorMessage = "this field cannot be left empty")]
        [Display(Description = "In Stock ")]
        public int Quantity { get; set; }
    }
}

