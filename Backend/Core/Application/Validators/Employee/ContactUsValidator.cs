using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using FluentValidation;

namespace EcommerceBackend.Core.Application.Validators.EmployeeValidators
{
    public class ContactUsValidator:AbstractValidator<ContactUsSetDto>
    {
        public ContactUsValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name is required.")
            .MaximumLength(100).WithMessage("User name must not exceed 100 characters.");

            RuleFor(x => x.Account)
                .NotEmpty().WithMessage("Account (email) is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message cannot be empty.")
                .MaximumLength(1000).WithMessage("Message must not exceed 1000 characters.");

        }
    }
}
