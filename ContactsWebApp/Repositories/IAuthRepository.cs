using ContactsWebApp.Models;

namespace ContactsWebApp.Repositories
{
    public interface IAuthRepository
    {
        public Task<bool> IsEmailUsedAsync(string email); // check if email is already used

        public Task<bool> CreateUserAsync(AppUser appUser, string password); // create new user

        public Task<AppUser?> SignInAsync(string email, string password); // sign in

        public Task<IList<string>> GetUserRolesAsync(AppUser user); // get user roles
    }
}
