using EcommerceBackend.DTO_s.ClientDTO_s;
using FluentValidation;

namespace EcommerceBackend.Core.Application.Validators.ClientValidators
{
    public class SignUpClientValidator: AbstractValidator<ClientSignUpDto>
    {
        public SignUpClientValidator()
        {
            RuleFor(c => c).NotNull();
            RuleFor(c => c.Account_informations).NotNull();
            RuleFor(c => c.FirstName).NotEmpty().MaximumLength(30).WithMessage("Invalid First Name.");
            RuleFor(c => c.FirstName).NotEmpty().MaximumLength(30).WithMessage("Invalid Last Name.");
            RuleFor(c => c.Account_informations!.Account).NotEmpty();
            RuleFor(c => c.Account_informations!.Password).NotEmpty();
            RuleFor(c=>c.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
             .MaximumLength(30)
             .WithMessage("Invalid phone number format. Include country code if necessary.");

            RuleFor(c => c.Account_informations!.Account)
              .NotEmpty().WithMessage("Email is required.")
              .EmailAddress().WithMessage("Invalid email format.");


            RuleFor(c => c.Account_informations!.Password).MinimumLength(7).WithMessage("Password length must be greater than 7.");
            RuleFor(c => c.Account_informations!.Password).MaximumLength(20).WithMessage("Password length must be less than 20.");
        }
    }
}
