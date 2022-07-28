using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HBOnlineTyresApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name")]
        public string FName { get; set; }

      
    }
}
