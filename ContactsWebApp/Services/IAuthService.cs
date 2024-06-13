using ContactsWebApp.DTO;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace ContactsWebApp.Services
{
    public interface IAuthService
    {
        public bool IsRegisterModelValid(RegisterDto registerDto); // validation of register model
        public string GenerateJwtToken(IdentityUser user, IList<string> roles); // generate jwt token for logged user

        public bool IsValidEmail(string email); // check validation of email
        public bool IsPasswordStrong(string password); // check if password is strong
        public bool IsLoginModelValid(LoginDto loginDto); // check if login model is valid
    }
}
