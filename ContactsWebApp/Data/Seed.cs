using ContactsWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace ContactsWebApp.Data
{
    public class Seed
    {
        /*public static void SeedMovies(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope()) // create scope 
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // get db context

                if (!dbContext.Movies.Any())
                {
                    var movies = new List<Movie> // create list of entities to seed
                    {
                        new Movie {Description = "desc", ReleaseDate = DateTime.Now, Title = "numero uno" } 
                    };

                    dbContext.Movies.AddRange(movies); // add data
                    dbContext.SaveChanges();
                }
            }
        }*/

        public static async Task SeedRoles(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope()) // create scope 
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roleNames = { UserRoles.User, UserRoles.Admin};

                foreach (var roleName in roleNames)
                {
                    var roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "cos@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "cos@gmail.com",
                        Email = adminUserEmail,
                        FirstName = "piter",
                        LastName = "rusek"
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
