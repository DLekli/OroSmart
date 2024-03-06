using FluentValidation;
using OroSmart.Models;

namespace OroSmart.Data.Validator
{
    public class ContactValidator : AbstractValidator<CustomersContacts>
    {
        public ContactValidator()
        {
            RuleFor(x => x.ContactTypeId).NotEmpty().WithMessage("Contact Type is required.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName of registration is required.");
            RuleFor(x => x.Contact).NotEmpty().WithMessage("Contact of registration is required.");
        }
    }
}
