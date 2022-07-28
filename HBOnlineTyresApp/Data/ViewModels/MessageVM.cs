using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class MessageVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Description { get; set; }

        [Display(Name = "Viewed")]
        public bool? Viewed { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
