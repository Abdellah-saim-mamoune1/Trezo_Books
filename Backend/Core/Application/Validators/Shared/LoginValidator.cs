using EcommerceBackend.DTO_s;
using FluentValidation;

namespace EcommerceBackend.Core.Application.Validators.SharedValidators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {

        public LoginValidator()
        {
            RuleFor(x => x.Account)
                .NotEmpty().WithMessage("Account is required.")
                .EmailAddress().WithMessage("Invalid email format."); // إذا كان الـ Account هو بريد إلكتروني

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.");
        }
    }
}
