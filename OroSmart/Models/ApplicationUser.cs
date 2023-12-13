using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OroSmart.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string LastLoginIpAddress { get; set; }
    }
}
