using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class LoginVM
    {
        [Required (ErrorMessage = "A Valid Email Address is Required")]
        [Display(Name = "Email Address")]
        public  string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
