using System.ComponentModel.DataAnnotations;

namespace OroSmart.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string VAT { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public bool Active { get; set; }

        // Foreign key for the user who added the customer
        public string first_entry_user_id { get; set; }
        public ApplicationUser CustomersAdded { get; set; }

        // Foreign key for the user who last updated the customer
        public string last_update_user_id { get; set; }
        public ApplicationUser CustomersLastUpdated { get; set; }

        public DateTime last_update_Timestamp { get; set; }

        public CustomersWorkLocation? WorkLocation { get; set; }

        public List<CustomersContacts>? CustomersContacts { get; set; }


    }

}
