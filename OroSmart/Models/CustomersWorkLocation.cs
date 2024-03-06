using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OroSmart.Models
{
    public class CustomersWorkLocation
    {
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Headquarters")]
        public bool IsHeadquarters { get; set; }
        
        [ForeignKey("ReferencePerson")]
        public string?  ReferencePersonId { get; set; }
        public ApplicationUser? ReferencePerson { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}
