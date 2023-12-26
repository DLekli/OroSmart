using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OroSmart.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }
<<<<<<< HEAD
        //public DateTime LastLoginTime { get; set; }
        //public string LastLoginIpAddress { get; set; }
        public virtual ICollection<UserLoginHistory> LoginHistory { get; set; }


=======
        public DateTime LastLoginTime { get; set; }
        public string? LastLoginIpAddress { get; set; }
        public string Language {  get; set; } = "en-US";
>>>>>>> Semi
    }
}
