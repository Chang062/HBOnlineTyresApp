using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Data.ViewModels
{
    public class ResetPasswordVM
    {
        public string Token { get; set; }
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
