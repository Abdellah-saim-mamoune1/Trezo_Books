using EcommerceBackend.Core.Application.DTO_s.EmployeeDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.ClientXEmployeeModels;
using EcommerceBackend.Core.Domain.Models.EmployeeModels;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.EmployeeDTO_s;
using EcommerceBackend.DTO_s.EmployeeXClientDTO_s;
using EcommerceBackend.Infrastructure.Data;
using EcommerceBackend.UtilityClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.EmployeeRepositories
{
    public class EmployeeRepository(AppDbContext _db):IEmployeeRepository
    {
        public async Task<TokenResponseDto?> RegisterAsync(EmployeeSignUpDto SignUpInfos)
        {
            string FirstName = SignUpInfos.Person_informations!.FirstName.Replace(" ", "");
            string LastName = SignUpInfos.Person_informations.LastName.Replace(" ", "");
            string account = FirstName + "." + LastName + "@Trezo.com";

            var Tokens = new TokenResponseDto(); 

            if (await _db.EmployeeAccount.AnyAsync(e=>e.AccountAddress==account))
                return null;

            string password = new PasswordHasher<EmployeeSignUpDto>()
              .HashPassword(SignUpInfos, SignUpInfos!.Password);
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    int personId = await RegisterPerson(SignUpInfos);
                    Tokens= UMethods.CreateTokenResponse(personId.ToString(), SignUpInfos.Role);
                    await _db.SaveChangesAsync();

                    int tokenId = await RegisterToken(Tokens.RefreshToken);
                    int AccountId = await RegisterAccount(account, password, SignUpInfos.Role, tokenId);

                    _db.Employees.Add(new Employee
                    {
                        PersonId = personId,
                        AccountId = AccountId
                    });
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                });

                return Tokens;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Employee registration transaction failed: {ex.Message}");
                return null;
            }
        }


        public async Task<bool> ResetPasswordAsync(ResetPasswordDto data, int ClientId)
        {
            string password = new PasswordHasher<ResetPasswordDto>()
              .HashPassword(data, data.NewPassword);
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    var account = await _db.Employees.AsQueryable().Include(c => c.EmployeeAccount).FirstAsync(C => C.PersonId == ClientId);
                    account!.EmployeeAccount!.Password = password;
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                });

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Password reset transaction failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(PersonDto form,int Id)
        {
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var employee = await _db.Employees.Include(e => e.Person).AsQueryable().FirstAsync(e => e.PersonId == Id);
                    employee.Person!.FirstName = form.FirstName;
                    employee.Person.LastName = form.LastName;
                    employee.Person.PhoneNumber = form.PhoneNumber;
                    employee.Person.Gender = form.Gender;
                    employee.Person.BirthDate = form.BirthDate;
                    employee.Person.Address=form.Address;

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();

                });

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Employee update transaction failed: {ex.Message}");
                return false;
            }
        }



        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var Employee = await _db.Employees.AsQueryable().Include(e=>e.EmployeeAccount).ThenInclude(e=>e!.token).FirstAsync(e => e.PersonId == Id);
                var EmployeeAccount = await _db.EmployeeAccount.FirstAsync(e => e.Id == Employee.AccountId);
                var Token = await _db.Tokens.FirstAsync(t => t.Id == Employee!.EmployeeAccount!.token!.Id);
                var person = await _db.Persons.FirstAsync(p => p.Id == Employee.PersonId);

                _db.Remove(Employee);
                await _db.SaveChangesAsync();
                _db.Remove(person);
                await _db.SaveChangesAsync();
                _db.Remove(EmployeeAccount);
                await _db.SaveChangesAsync();
                _db.Remove(Token);
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Employee deletion process failed: {ex.Message}");
                return false;
            }
        }

        

        public async Task<List<GetEmployeesDto>> GetAllAsync()
        {
            var data = await _db.Employees.Include(e => e.Person).Include(e => e.EmployeeAccount).ThenInclude(e => e!.EmployeeAccountType)
                .AsQueryable().Select(e => new GetEmployeesDto
                {
                    Id=e.PersonId,
                    Address = e.Person!.Address!,
                    BirthDate = e.Person.BirthDate,
                    Email = e.EmployeeAccount!.AccountAddress,
                    FirstName = e.Person.FirstName,
                    LastName = e.Person.LastName,
                    Gender = e.Person!.Gender!,
                    PhoneNumber = e.Person.PhoneNumber,
                    Type = e.EmployeeAccount!.EmployeeAccountType!.TypeName
                }).ToListAsync();

            return data;
        }

        public async Task<GetEmployeesDto> GetByIdAsync(int Id)
        {
            var data = await _db.Employees.Include(e => e.Person).Include(e => e.EmployeeAccount).ThenInclude(e => e!.EmployeeAccountType)
                .AsQueryable().Where(e=>e.PersonId==Id).Select(e => new GetEmployeesDto
                {
                    Id=e.PersonId,
                    Address = e.Person!.Address!,
                    BirthDate = e.Person.BirthDate,
                    Email = e.EmployeeAccount!.AccountAddress,
                    FirstName = e.Person.FirstName,
                    LastName = e.Person.LastName,
                    Gender = e.Person!.Gender!,
                    PhoneNumber = e.Person.PhoneNumber,
                    Type = e.EmployeeAccount!.EmployeeAccountType!.TypeName
                }).FirstAsync();

            return data;
        }

        private async Task<int> RegisterPerson(EmployeeSignUpDto SignUpInfos)
        {

            Person person = new Person
            {
                FirstName = SignUpInfos.Person_informations!.FirstName.Trim(),
                LastName = SignUpInfos.Person_informations.LastName.Trim(),
                Address = SignUpInfos.Person_informations.Address.Trim(),
                PhoneNumber = SignUpInfos.Person_informations.PhoneNumber.Trim(),
                BirthDate = SignUpInfos.Person_informations.BirthDate,
                Gender=SignUpInfos.Person_informations.Gender

            };
            _db.Persons.Add(person);
            await _db.SaveChangesAsync();
            return person.Id;

        }

        private async Task<int> RegisterToken(string RefreshToken)
        {
            Token token = new Token
            {
                RefreshToken = RefreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30)
            };

            _db.Tokens.Add(token);
            await _db.SaveChangesAsync();
            return token.Id;
        }

        private async Task<int> RegisterAccount(string account,string password,string role,int TokenId)
        {

            int roleId=await _db.EmployeeAccountTypes.AsQueryable().Where(r=>r.TypeName==role).Select(e => e.Id).FirstAsync();
            EmployeeAccount Account = new EmployeeAccount
            {
                AccountAddress = account,
                Password = password,
                TokenId = TokenId,
                AccountTypeId = roleId,
                IsFrozen = false
            };

            _db.EmployeeAccount.Add(Account);

            await _db.SaveChangesAsync();
            return Account.Id;
        }

      
    }
}
