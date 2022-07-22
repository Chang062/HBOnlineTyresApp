using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Full Name is Required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required (ErrorMessage = "A Valid Email Address is Required")]
        [Display(Name = "Email Address")]
        public  string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        
        
    }
}
