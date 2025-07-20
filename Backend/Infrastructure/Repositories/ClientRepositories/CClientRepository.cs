using EcommerceBackend.Core.Application.DTO_s.ClientDTO_s;
using EcommerceBackend.Core.Application.DTO_s.ClientManagementDTO_s;
using EcommerceBackend.Core.Application.DTO_s.SharedDTO_s;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Models.ClientModels;
using EcommerceBackend.Core.Domain.Models.ClientXEmployeeModels;
using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using EcommerceBackend.DTO_s.ClientDTO_s;
using EcommerceBackend.Infrastructure.Data;
using EcommerceBackend.UtilityClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackend.Infrastructure.Repositories.ClientRepositories
{
    public class CClientRepository(AppDbContext _db): ICClientManagementRepository
    {

        public async Task<DTokenResponse?> RegisterClientAsync(DCClientSignUp SignUpInfos)
        {
            var Tokens=new DTokenResponse();
            string password = new PasswordHasher<DCClientSignUp>()
              .HashPassword(SignUpInfos, SignUpInfos!.Account_informations!.Password);
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    int personId = await SignUpPerson(SignUpInfos);
                    Tokens = UMethods.CreateTokenResponse(personId.ToString(), "Client");
                    int TokenId = await RegisterRefreshToken(Tokens.RefreshToken);
                    int AccountId = await RegisterAccount(SignUpInfos!.Account_informations!.Account, password, TokenId);
                    await RegisterClient(personId, AccountId);

                    await transaction.CommitAsync();
                    
                });

                return Tokens;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client Registration Transaction failed: {ex.Message}");
                return null;
            }
        }


        public async Task<bool> ResetPasswordAsync(DResetPassword data,int ClientId)
        {
            string password = new PasswordHasher<DResetPassword>()
              .HashPassword(data, data.NewPassword);
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();
                    var account = await _db.Clients.AsQueryable().Include(c => c.Account).FirstAsync(C => C.PersonId == ClientId);
                    account!.Account!.Password=password;
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

        public async Task<bool> UpdateProfileInfoAsync(DCUpdateClientProfileInfo Form, int Id)
        {
            bool Success = false;
            try
            {
                await _db.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var transaction = await _db.Database.BeginTransactionAsync();

                    var Client = await _db.Clients.AsQueryable().Include(C => C!.Person).FirstAsync(c => c.PersonId == Id);
                    Client!.Person!.FirstName = Form.FirstName;
                    Client.Person.LastName = Form.LastName;
                    Client.Person.PhoneNumber = Form.PhoneNumber;

                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                });

                Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client Update Transaction failed: {ex.Message}");

            }
            return Success;
        }

        public async Task<DCGetClientInfo?> GetClientInfoByIdAsync(int ClientId)
        {
            try
            {

                return await _db.Clients.Include(c => c.Person).Include(c => c.Account)
                    .AsQueryable().Where(c => c.PersonId == ClientId).Select(c => new DCGetClientInfo
                    {
                        FirstName = c.Person!.FirstName,
                        LastName = c.Person.LastName,
                        PhoneNumber = c.Person.PhoneNumber,
                        Account = c.Account!.Account
                    }).FirstAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fetching client info process failed: {ex.Message}");
                return null;
            }

        }

        private async Task<int> SignUpPerson(DCClientSignUp SignUpInfos)
        {

            Person person = new Person
            {

                FirstName = SignUpInfos.FirstName.Trim(),
                LastName = SignUpInfos.LastName.Trim(),
                Address = null,
                PhoneNumber = SignUpInfos.PhoneNumber.Trim(),
                BirthDate = null,

            };

            _db.Persons.Add(person);
            await _db.SaveChangesAsync();

            return person.Id;
        }

        private async Task<int> RegisterRefreshToken(string RefreshToken)
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

        private async Task<int> RegisterAccount(string Account,string password,int TokenId)
        {
             ClientAccount CAccount = new ClientAccount
            {
                Account = Account,
                Password = password,
                TokenId = TokenId,
            };
            _db.ClientsAccounts.Add(CAccount);
            await _db.SaveChangesAsync();
            return CAccount.Id;
        }

        private async Task RegisterClient(int personId,int AccountId)
        {
            _db.Clients.Add(new Client
            {
                PersonId = personId,
                AccountId = AccountId
            });
            await _db.SaveChangesAsync();
        }


    }
}
