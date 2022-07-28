using HBOnlineTyresApp.Models;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBOnlineTyresApp.Data.ViewModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class NewTyreVM
    {
        public int Id { get; set; }

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
