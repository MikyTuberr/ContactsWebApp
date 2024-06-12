using ContactsWebApp.Data;
using ContactsWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ContactsWebApp.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CreateUserAsync(AppUser newUser, string password)
        {
            var newUserResponse = await _userManager.CreateAsync(newUser, password); // create new user

            /*if (newUserResponse.Succeeded)
            {
                var addToRoleResult = await _userManager.AddToRoleAsync(newUser, UserRoles.User); // assign role to user as normal user
                if (!addToRoleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(newUser); // if adding role was unsucessful delete new user
                    return false;
                }
            }*/
            return true;
        }

        public async Task<bool> IsEmailUsedAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email); // check if email is already used
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<AppUser?> SignInAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false); // try to sign in

            // If login is successful, return the user
            if (result.Succeeded)
            {
                var appUser = await _userManager.FindByEmailAsync(email);
                return appUser;
            }
            return null;
        }

        public async Task<IList<string>> GetUserRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

    }
}
