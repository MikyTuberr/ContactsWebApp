using ContactsWebApp.Models;

namespace ContactsWebApp.Repositories
{
    public interface IAuthRepository
    {
        public Task<bool> IsEmailUsedAsync(string email);

        public Task<bool> CreateUserAsync(AppUser appUser, string password);

        public Task<AppUser?> SignInAsync(string email, string password);

        public Task<IList<string>> GetUserRolesAsync(AppUser user); // get user roles
    }
}
