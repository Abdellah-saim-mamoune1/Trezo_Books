using EcommerceBackend.Core.Application.DTO_s.AuthorDTO_s;
using FluentValidation;

namespace EcommerceBackend.Core.Application.Validators.EmployeeValidators.AuthorValidators
{
    public class AuthorValidator:AbstractValidator<AuthorDto>
    {
        public AuthorValidator() 
        {
            RuleFor(a => a.FullName)
                    .NotEmpty().WithMessage("Author name is required.")
                    .MaximumLength(50).WithMessage("Author name must not exceed 50 characters.");
           
        }
    }
}
         
      