using OroSmart.Models;
using System.ComponentModel.DataAnnotations;

namespace OroSmart.Data.ViewModels
{
    public class CustomerViewModel
    {
        public Customer? Customer { get; set; }
        public CustomersWorkLocation? WorkLocation { get; set; }

       // [Required]

        public CustomersContacts? CustomersContacts { get; set; }
        public ContactType? ContactTypes { get; set; }
    }
}
