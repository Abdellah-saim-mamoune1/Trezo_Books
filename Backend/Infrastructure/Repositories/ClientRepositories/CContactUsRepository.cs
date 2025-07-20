using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.EmployeeModels;
using EcommerceBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.ClientRepositories
{
    public class CContactUsRepository(AppDbContext _db): ICContactUsRepository
    {
        public async Task<bool> Create(DEContactUsSet form)
        {
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    var data = new ContactUs
                    {
                        UserName = form.UserName,
                        Account = form.Account,
                        Message = form.Message,
                    };
                    _db.Add(data);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                });
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Adding message process failed " + ex.ToString());
                return false;
            }
        }
    }
}
