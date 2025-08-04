using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.EmployeeModels;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EcommerceBackend.Infrastructure.Repositories.EmployeeRepositories
{
    public class ContactUsRepository(AppDbContext _db): IContactUsRepository
    {

        public async Task<List<ContactUs>> Get()
        {
            return await _db.ContactUs.ToListAsync();
        }


        public async Task<bool> Delete(int Id)
        {
            try
            {
                    var ContactUs =await _db.ContactUs.AsQueryable().FirstAsync(c => c.Id == Id);
                    _db.Remove(ContactUs);
                    await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Deleting message process failed " + ex.ToString());
                return false;
            }
        }

      
    }
}
