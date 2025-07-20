using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s.BookDTO_s;
using FluentValidation;

public class AddBookValidator : AbstractValidator<DEAddBook>
{
    public AddBookValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150).WithMessage("Title must not exceed 150 characters.");

        RuleFor(b => b.Author)
            .NotEmpty().WithMessage("Author is required.")
            .MaximumLength(100).WithMessage("Author must not exceed 100 characters.");

        RuleFor(b => b.Type)
            .NotEmpty().WithMessage("Type is required.")
            .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");

        RuleFor(b => b.Language)
            .NotEmpty().WithMessage("Language is required.")
            .Matches("^[a-z]{2}$").WithMessage("Language must be a 2-letter lowercase code (e.g. 'en', 'fr').");
    }
}
