using HBOnlineTyresApp.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBOnlineTyresApp.Models
{
    [Index(nameof(SpecsId),IsUnique = true)]
    public class Inventory:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public int SpecsId { get; set; }
        [ForeignKey("SpecsId")]
        public Specification Specifications { get; set; }
        [Display(Name = "In Stock")]
        public int Quantity { get; set; }
    }
}
