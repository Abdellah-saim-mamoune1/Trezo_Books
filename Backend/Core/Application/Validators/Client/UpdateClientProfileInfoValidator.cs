using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using FluentValidation;

namespace EcommerceBackend.Core.Application.Validators.ClientValidators
{
    public class UpdateClientProfileInfoValidator : AbstractValidator<UpdateClientProfileInfoDto>
    {

        public UpdateClientProfileInfoValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");
          
        }
    }
}
