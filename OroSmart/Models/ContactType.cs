using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OroSmart.Models
{
    public class ContactType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Note { get; set; }

        public CustomersContacts? CustomersContacts { get; set; }
    }
}
