using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; }
    }
}
