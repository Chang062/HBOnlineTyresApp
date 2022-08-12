using HBOnlineTyresApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Models
{
    public class Manufacturer: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required (ErrorMessage = "Name of Manufacturer is required")]
        public string Name { get; set; }
        [Display(Name = "Logo URL")]
        [Required(ErrorMessage = "Logo URL is required")]
        public string LogoURL { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "This field cannot be left empty")]
        public string Description { get; set; }
    }
}
