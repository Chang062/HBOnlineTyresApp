using HBOnlineTyresApp.Models;
using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class NewTyreVM
    {

        [Display(Name = "Model")]
        [Required(ErrorMessage = "This field cannot be left empty")]
        public string Name { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "This field cannot be left empty")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "This field cannot be left empty")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "This field cannot be left empty")]
        public string ImageURL { get; set; }

        [Display(Name = "Manufacturer")]
        [Required(ErrorMessage = "This field cannot be left empty")]
        public int ManufacturerId { get; set; }

    }
}
