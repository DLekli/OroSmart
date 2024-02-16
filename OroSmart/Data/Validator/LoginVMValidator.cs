using FluentValidation;
using OroSmart.Data.ViewModels;

namespace OroSmart.Data.Validator
{
    public class LoginVMValidator : AbstractValidator<LoginVM>
    {
        public LoginVMValidator()
        {
            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email Address is required.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
        }
    }
}
