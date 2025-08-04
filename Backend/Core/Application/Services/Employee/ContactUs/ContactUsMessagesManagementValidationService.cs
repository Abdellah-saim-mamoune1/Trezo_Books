using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Application.Validators.EmployeeValidators;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.ContactUsManagementServicesInterfaces;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Core.Application.Services.EmployeeServices.EContactUsServices
{
    public class ContactUsMessagesManagementValidationService(AppDbContext _db): IContactUsMessagesManagementValidationService
    {
        public List<ValidationErorrsDto>? ValidateSet(ContactUsSetDto form)
        {
            var validator = new ContactUsValidator();
            var result = validator.Validate(form);
            if (!result.IsValid)
                return result.Errors.Select(e => new ValidationErorrsDto { FieldId = e.PropertyName, Message = e.ErrorMessage }).ToList();
            return null;
        }

        public async Task<ValidationErorrsDto?> ValidateDelete(int MessageId)
        {
            
            if (!await _db.ContactUs.AsQueryable().AnyAsync(c=>c.Id==MessageId))
                return  new ValidationErorrsDto { FieldId = "MessageId", Message = "Message not found." };

            return null;
        }
    }
}
