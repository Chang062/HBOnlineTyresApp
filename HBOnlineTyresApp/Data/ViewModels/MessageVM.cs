using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class MessageVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sender")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email or Contact No.")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Description { get; set; }

        [Display(Name = "Read")]
        public bool? Viewed { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
