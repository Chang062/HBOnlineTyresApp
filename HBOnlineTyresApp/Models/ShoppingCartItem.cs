using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBOnlineTyresApp.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Inventory Inventory { get; set; }
        public int Amount { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
