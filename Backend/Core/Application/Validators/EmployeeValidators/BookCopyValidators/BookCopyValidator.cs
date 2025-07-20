using EcommerceBackend.Core.Application.DTO_s.BookCopyDTO_s;
using FluentValidation;

namespace EcommerceBackend.Core.Application.Validators.EmployeeValidators.BookCopyValidators
{
    public class BookCopyValidator : AbstractValidator<DEBookCopy>
    {

        public BookCopyValidator() 
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0)
                .WithMessage("Book ID must be a positive number.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity cannot be negative.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.IsAvailable)
                .Must((dto, isAvailable) => !isAvailable || dto.Quantity > 0)
                .WithMessage("Book cannot be marked as available if quantity is 0.");

        }
    }
}
