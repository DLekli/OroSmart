using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OroSmart.Models
{
    public class CustomersContacts
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        
       
        [ForeignKey("ContactTypeId")]
        public int? ContactTypeId { get; set; }
        public ContactType? ContactType { get; set; }

        [Required(ErrorMessage = "First name is required")]

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        
        public string Contact { get; set; }

        public string Note { get; set; }
    }
}
