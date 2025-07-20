using EcommerceBackend.Core.Application.Services.AuthenticationServices;
using EcommerceBackend.Core.Application.Services.ClientServices.CBookCopyServices;
using EcommerceBackend.Core.Application.Services.ClientServices.CCartServices;
using EcommerceBackend.Core.Application.Services.ClientServices.ClientManagementServices;
using EcommerceBackend.Core.Application.Services.ClientServices.COrderServices;
using EcommerceBackend.Core.Application.Services.ClientServices.CWishlistServices;
using EcommerceBackend.Core.Application.Services.EmployeeServices.EAuthorServices;
using EcommerceBackend.Core.Application.Services.EmployeeServices.EBookCopyServices;
using EcommerceBackend.Core.Application.Services.EmployeeServices.EBookServices;
using EcommerceBackend.Core.Application.Services.EmployeeServices.EContactUsServices;
using EcommerceBackend.Core.Application.Services.EmployeeServices.EEmployeeManagementServices;
using EcommerceBackend.Core.Application.Services.EmployeeServices.EOrdersServices;
using EcommerceBackend.Core.Application.Services.EmployeeServices.EStatisticsServices;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.AuthenticationRepositories;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.RepositoriesInterfaces.EmployeeRepositoriesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CBookCopyServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CCartServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.ClientManagementServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.COrderServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.ClientServicesInterfaces.CWishlistServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.AuthorServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.BookServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.ContactUsManagementServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EBookCopyServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EEmployeeManagementServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.EOrdersServicesInterfaces;
using EcommerceBackend.Core.Domain.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces.StatisticsInterfaces;
using EcommerceBackend.Infrastructure.Data;
using EcommerceBackend.Infrastructure.Repositories.AuthenticationRepositories;
using EcommerceBackend.Infrastructure.Repositories.ClientRepositories;
using EcommerceBackend.Infrastructure.Repositories.EmployeeRepositories;
using EcommerceBackend.UtilityClasses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();


builder.Services.AddScoped<IAuthService, AuthenticationService>();
builder.Services.AddScoped<ICClientRegistrationService, ClientManagementService>();
builder.Services.AddScoped<IEEmployeeManagementService, EEmployeeManagementService>();

builder.Services.AddScoped<IEAuthorManagementValidationService, EAuthorManagementValidationService>();
builder.Services.AddScoped<IEAuthorManagementService, EAuthorManagementService>();
builder.Services.AddScoped<IEBookManagementService, EBookManagementService>();
builder.Services.AddScoped<IEBookManagementValidationService, EBookManagementValidationService>();
builder.Services.AddScoped<IEBookCopyManagementValidationService, EBookCopyManagementValidationService>();
builder.Services.AddScoped<IEBookCopyManagementService, EBookCopyManagementService>();
builder.Services.AddScoped<ICCartManagementService, CCartManagementService>();
builder.Services.AddScoped<ICCartManagementValidationService, CCartManagementValidationService>();
builder.Services.AddScoped<ICClientManagementValidatorsService, CClientManagementValidatorsService>();


builder.Services.AddScoped<ICGetBooksCopiesService, CGetBooksCopiesService>();
builder.Services.AddScoped<ICOrderManagementValidationService, COrderManagementValidationService>();
builder.Services.AddScoped<ICOrderManagementService, COrderManagementService>();
builder.Services.AddScoped<IEOrdersManagementValidationService, EOrdersManagementValidationService>();
builder.Services.AddScoped<IEOrdersManagementService, EOrdersManagementService>(); 
builder.Services.AddScoped<ICWishlistManagementValidationService, CWishlistManagementValidationService>();
builder.Services.AddScoped<ICWishlistManagementService, CWishlistManagementService>();
builder.Services.AddScoped<IEContactUsMessagesManagementValidationService, EContactUsMessagesManagementValidationService>();
builder.Services.AddScoped<IEContactUsMessagesManagementService, EContactUsMessagesManagementService>();
builder.Services.AddScoped<IEStatisticsService, EStatisticsService>(); 
builder.Services.AddScoped<IAuthenticationValidationService, AuthenticationValidationService>();

builder.Services.AddScoped<ICClientManagementRepository, CClientRepository>();
builder.Services.AddScoped<IEEmployeeRepository, EEmployeeRepository>();
builder.Services.AddScoped<IEAuthorRepository, EAuthorRepository>();
builder.Services.AddScoped<IEBookRepository, EBookRepository>();
builder.Services.AddScoped<IEBookCopyRepository, EBookCopyRepository>();
builder.Services.AddScoped<ICCartRepository, CCartRepository>();
builder.Services.AddScoped<ICBookCopyRepository, CBookCopyRepository>();
builder.Services.AddScoped<ICOrderRepository, COrderRepository>(); 
builder.Services.AddScoped<IEGetPaginatedOrders, EOrdersRepository>(); 
builder.Services.AddScoped<ICWishlistRepository, CWishlistRepository>(); 
builder.Services.AddScoped<IEContactUsRepository, EContactUsRepository>(); 
builder.Services.AddScoped<ICContactUsRepository, CContactUsRepository>();
builder.Services.AddScoped<IEStatisticsRepository, EStatisticsRepository>();
builder.Services.AddScoped<ITokensRepository, TokensRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
UMethods.SetConfiguration(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>


options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),

        };


      
    });


builder.Services.AddAuthorization();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("https://trezo-ruddy.vercel.app")
                  .AllowCredentials()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowReactApp");
// Configure the HTTP request pipeline.


app.UseHttpsRedirection();


//Extracting  JWT from the cookie and append it to the api header instead of storing JWT in local storage
app.Use(async (context, next) =>
{
    var accessToken = context.Request.Cookies["accessToken"];
    if (!string.IsNullOrEmpty(accessToken) &&
        !context.Request.Headers.ContainsKey("Authorization"))
    {
        context.Request.Headers.Append("Authorization", $"Bearer {accessToken}");
    }

    await next();
});

var MainProductImagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images","ProductsImages","MainProductImages");
if (!Directory.Exists(MainProductImagesPath))
{
    Directory.CreateDirectory(MainProductImagesPath);
}

var ProductColorsImagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images", "ProductsImages", "ProductColorsImages");
if (!Directory.Exists(ProductColorsImagesPath))
{
    Directory.CreateDirectory(ProductColorsImagesPath);
}
app.UseAuthentication();


//for Images 
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(MainProductImagesPath),
    RequestPath = "/MainProductsImages"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(MainProductImagesPath),
    RequestPath = "/ProductColorsImages"
});


app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
  
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();













