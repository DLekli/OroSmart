using Microsoft.AspNetCore.Identity;
using OroSmart.Data.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace OroSmart.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        public virtual ICollection<UserLoginHistory> LoginHistory { get; set; }

        public string Language {  get; set; } = "en-US";
        
        public ICollection<Customer> CustomersAdded { get; set; }

        public ICollection<Customer> CustomersLastUpdated { get; set; }
    }
}
