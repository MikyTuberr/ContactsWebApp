using ContactsWebApp.Data;
using ContactsWebApp.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace ContactsWebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsRegisterModelValid(RegisterDto registerDto)
        {
            if (registerDto.Email == null) { return false; }
            if (registerDto.Password == null) { return false; }
            if (registerDto.FirstName == null) { return false; }
            if (registerDto.LastName == null) { return false; }
            return true;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Regex for validation of email
            string emailPattern = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            // Validate email
            return Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase);
        }

        public bool IsPasswordStrong(string password)
        {
            // Check if password is longer than 16 
            if (string.IsNullOrEmpty(password) || password.Length < 16)
                return false;

            // Check if passowrd contains uppercase letter, lowercase letter, number and special character
            bool containsSpecialCharacters = Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]+");
            bool containsUpperCase = Regex.IsMatch(password, @"[A-Z]+");
            bool containsLowerCase = Regex.IsMatch(password, @"[a-z]+");
            bool containsNumber = Regex.IsMatch(password, @"[0-9]+");

            return containsSpecialCharacters && containsUpperCase && containsLowerCase && containsNumber;
        }

        // Generate JWT token for the user
        public string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            // Ensure that user object is not null
            if (user == null) { throw new ArgumentNullException(nameof(user)); }

            // Define JWT claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // Add roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Generate symmetric security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Generate JWT token with claims, expiration date, and signing credentials
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds
            );

            // Serialize JWT token to string and return it
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsLoginModelValid(LoginDto loginDto)
        {
            if (loginDto.Email == null) { return false; }
            if (loginDto.Password == null) { return false; }
            return true;
        }
    }
}
