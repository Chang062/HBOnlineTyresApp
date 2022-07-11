using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; } 
        public int Amount { get; set; }
        public double Price{ get; set; }
        public int InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }
        public int OderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
