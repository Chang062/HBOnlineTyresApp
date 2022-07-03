using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBOnlineTyresApp.Models
{
    public class Tyre
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Model")]
        public string Name { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category category { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string ImageURL { get; set; }
        //Relationship
        [Display(Name = "Manufacturer ID")]
        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
    }
}
