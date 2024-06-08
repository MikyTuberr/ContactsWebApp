using ContactsWebApp.DTO;
using Microsoft.AspNetCore.Identity;

namespace ContactsWebApp.Services
{
    public interface IAuthService
    {
        public bool IsRegisterModelValid(RegisterDto registerDto);
        public string GenerateJwtToken(IdentityUser user, IList<string> roles);

        public bool IsLoginModelValid(LoginDto loginDto);
    }
}
