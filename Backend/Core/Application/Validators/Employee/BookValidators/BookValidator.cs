using EcommerceBackend.Core.Application.DTO_s.BookDTO_s;
using FluentValidation;

namespace EcommerceBackend.Core.Application.Validators.EmployeeValidators.BookValidators
{
    public class BookValidator : AbstractValidator<BookDto>
    {
        public BookValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Book name is required.")
                .MaximumLength(200).WithMessage("Book name must not exceed 200 characters.");

            RuleFor(b => b.TypeId)
                .GreaterThan(0).WithMessage("Type ID must be greater than 0.");

            RuleFor(b => b.AuthorId)
                .GreaterThan(0).WithMessage("Author ID must be greater than 0.");

            RuleFor(b => b.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(b => b.PagesNumber)
           .GreaterThan(0)
           .WithMessage("PagesNumber must be positive value.");
            



            RuleFor(b => b.PublishedAt)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Published date cannot be in the future.");
        }
    }
}
