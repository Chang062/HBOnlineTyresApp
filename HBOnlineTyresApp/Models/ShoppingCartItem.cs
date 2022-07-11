using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Inventory Inventory { get; set; }
        public int Amount { get; set; }

        public string ShoppingCartId { get; set; } 
    }
}
