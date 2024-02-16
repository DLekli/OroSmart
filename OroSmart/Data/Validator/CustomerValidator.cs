using FluentValidation;
using OroSmart.Models;

namespace OroSmart.Data.Validator
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.VAT).NotEmpty().WithMessage("VAT is required.");
            RuleFor(x => x.DateOfRegistration).NotEmpty().WithMessage("Date of registration is required.");
        }
    }
}
