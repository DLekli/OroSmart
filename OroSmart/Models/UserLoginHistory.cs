using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OroSmart.Models
{
    public class UserLoginHistory
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public string FullName { get; set; }
        public DateTime? LoginTime { get; set; }

        public string? LoginIpAddress { get; set; }

        public DateTime? LogoutTime { get; set; }

    }
}
