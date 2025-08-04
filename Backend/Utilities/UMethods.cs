using EcommerceBackend.DTO_s.AuthunticationDTO_S;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EcommerceBackend.UtilityClasses
{
    public static class UMethods
    {

        static private IConfiguration? configuration;
        public static void SetConfiguration(IConfiguration config)
        {
            configuration = config;
        }

        private static readonly Random _random = new Random();
       

        public static TokenResponseDto CreateTokenResponse(string Id, string role)
        {
            return new TokenResponseDto
            {
                Role =role,
                AccessToken = CreateToken(Id, role),
                RefreshToken = GenerateRefreshToken()
            };
        }

        public static string CreateToken(string Id, string Role)
        {
            var claims = new List<Claim>
            {

                new Claim(ClaimTypes.NameIdentifier, Id),
                new Claim(ClaimTypes.Role, Role),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: configuration["AppSettings:Issuer"],
                audience: configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }




        public static bool ContainsNullOrEmpty(object? obj)
        {
            if (obj == null) return true;

            Type type = obj.GetType();


            if (obj is string str)
                return string.IsNullOrWhiteSpace(str);

            if (type.IsPrimitive || obj is DateTime || obj is decimal)
                return false;

            // Collections
            if (obj is IEnumerable<object> list)
            {
                foreach (var item in list)
                {
                    if (ContainsNullOrEmpty(item))
                        return true;
                }
                return false;
            }

            // Check properties
            foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                object? value;
                try
                {
                    value = prop.GetValue(obj);
                }
                catch
                {
                    continue;
                }

                if (value == null || ContainsNullOrEmpty(value))
                    return true;
            }

            return false;
        }


        public static string GenerateId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static bool ValidateAge(DateOnly BirthDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var age = today.Year - BirthDate.Year;

            // Adjust if the birthday hasn't happened yet this year
            if (BirthDate > today.AddYears(-age))
            {
                age--;
            }

            return age > 18;

        }



        public static bool CheckProductListQualities(Dictionary<int, string> dbitems, List<string> items)
        {
            bool check = true;
            foreach (var size in items)
            {
                if (!dbitems.ContainsValue(size))
                {
                    check = false;
                    break;
                }
            }
            return check;
        }

        public static bool ValidatePrice(List<int>? prices, int MinimumValue)
        {
            if (prices == null)
                return false;
            foreach (var item in prices)
            {
                if (item < MinimumValue)
                {
                    return false;
                }
            }
            return true;
        }

        public  static bool DoesImageExist(string image,string path)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), path, image);
            return  System.IO.File.Exists(fullPath);

        }

        public static void DeleteProductImageFromDisk(string SubDirectory, string ImageUrl)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var fileName = Path.GetFileName(new Uri(ImageUrl).LocalPath);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images", "ProductsImages", SubDirectory, fileName);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }


        public static string InsertProductImageToDisk(string SubDirectory,IFormFile Image)
        {

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images", "ProductsImages", SubDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                 Image.CopyToAsync(stream);
            }

            var ImageUrl = $"https://localhost:7042/ProductsImages/MainProductImages/{fileName}";
            return ImageUrl;
        }

        public static bool AreElementsUnique<T>(List<T> Items) where T : notnull
        {
            return Items.Count == Items.ToHashSet().Count;
        }



    }
}
