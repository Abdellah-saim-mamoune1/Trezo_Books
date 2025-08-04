using EcommerceBackend.DTO_s.SharedDTO_s;
using FluentValidation;

namespace EcommerceBackend.Core.Application.Validators.SharedValidators
{
    public class PaginationFormValidator: AbstractValidator<PaginationFormDto>
    {
        public PaginationFormValidator() 
        {
            RuleFor(p => p.pageNumber).GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than 0.");
            RuleFor(p => p.pageSize).GreaterThanOrEqualTo(1).WithMessage("Page size must be greater than 0.");
            RuleFor(p => p.pageSize).LessThanOrEqualTo(100).WithMessage("Page size must be less than 100.");
        }
    }
    
}
