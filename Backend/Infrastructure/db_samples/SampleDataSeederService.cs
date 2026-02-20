using System.Text.Json;
using EcommerceBackend.Core.Application.Services.EmployeeServices;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces;

namespace EcommerceBackend.Infrastructure.db_samples
{
    public class SampleDataSeederService
    {
        private readonly IClientRegistrationService _clientRepo;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IBookRepository _bookRepo;
        private readonly GoogleBooksImporterService _bookService;

        public SampleDataSeederService(
            IClientRegistrationService clientRepo,
            IEmployeeRepository employeeRepo,
            IBookRepository bookRepo,
            GoogleBooksImporterService bookService)
        {
            _clientRepo = clientRepo;
            _employeeRepo = employeeRepo;
            _bookRepo = bookRepo;
            _bookService = bookService;
        }

        public async Task SeedAsync(string json)
        {
          

            var sampleData = JsonSerializer.Deserialize<SampleDataModel>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (sampleData == null)
                return;

            // Seed Clients
            if (sampleData.clients != null)
            {
                foreach (var client in sampleData.clients)
                    await _clientRepo.SignUpClientAsync(client);
            }

            // Seed Employee Roles
            if (sampleData.employee_roles != null)
            {
                await _employeeRepo.AddEmployeeTypes(sampleData.employee_roles);
            }

            // Seed Employees
            if (sampleData.employees != null)
            {
                foreach (var employee in sampleData.employees)
                    await _employeeRepo.RegisterAsync(employee);
            }

            // Seed Book Types + Import Books
            if (sampleData.bookTypes != null)
            {
                foreach (var type in sampleData.bookTypes)
                {
                    await _bookRepo.CreateBookType(type);
                    await _bookService.ImportBooksAsync(type);
                }
            }
        }
    }
}
