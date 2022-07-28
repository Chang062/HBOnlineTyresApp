using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Models
{
    public class Category: IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category")]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
