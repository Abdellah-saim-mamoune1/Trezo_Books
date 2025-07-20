
using EcommerceBackend.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;
using FluentValidation;

public class RegisterEmployeeValidator : AbstractValidator<DEEmployeeSignUp>
{
    public RegisterEmployeeValidator()
    {
        RuleFor(x => x.Person_informations)
            .NotNull().WithMessage("Personal information is required.")
            .SetValidator(new DPersonValidator());

           


        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");

        RuleFor(c => c.Password).MinimumLength(7).WithMessage("Password length must be greater than 7.");
        RuleFor(c => c.Password).MaximumLength(20).WithMessage("Password length must be less than 20.");


       
    }
}

public class DPersonValidator : AbstractValidator<DPerson>
{
    public DPersonValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name can't exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name can't exceed 50 characters.");

        RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .Must(g => g == "Male" || g == "Female")
            .WithMessage("Gender must be either 'Male' or 'Female'.");

        RuleFor(x => x.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Today.AddYears(-18)))
            .WithMessage("Employee must be at least 18 years old.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number format. Include country code if necessary.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");
    }
}


