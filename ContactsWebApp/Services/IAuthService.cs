using ContactsWebApp.DTO;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace ContactsWebApp.Services
{
    public interface IAuthService
    {
        public bool IsRegisterModelValid(RegisterDto registerDto);
        public string GenerateJwtToken(IdentityUser user, IList<string> roles);

        public bool IsValidEmail(string email);
        public bool IsPasswordStrong(string password);
        public bool IsLoginModelValid(LoginDto loginDto);
    }
}
