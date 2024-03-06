using FluentValidation;
using OroSmart.Models;

namespace OroSmart.Data.Validator
{
    public class ContactTypeValidator : AbstractValidator<ContactType>
    {
        public ContactTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Note).NotEmpty().WithMessage("Note is required.");
        }
    }
}
