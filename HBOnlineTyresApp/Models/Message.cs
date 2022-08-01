using HBOnlineTyresApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Models
{
    public class Message: IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Sender")]
        public string FullName { get; set; }

        [Display(Name = "Email or Contact No.")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Message")]
        public string Description { get; set; }
        [Display(Name = "Date Received")]
        public DateTime DateReceived { get; set; }
        [Display(Name = "Read")]
        public bool? Viewed { get; set; }


    }
}
