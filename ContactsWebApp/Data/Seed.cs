using ContactsWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace ContactsWebApp.Data
{
    public class Seed
    {
        public static void SeedContacts(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope()) // create scope 
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // get db context

                if (!dbContext.Contacts.Any())
                {
                    var contacts = GenerateContacts(); // Generate contacts
                    dbContext.Contacts.AddRange(contacts); // add data
                    dbContext.SaveChanges();
                }
            }
        }

        private static Contact[] GenerateContacts()
        {
            var contacts = new Contact[]
            {
        new Contact
        {
            Name = "John",
            LastName = "Doe",
            Email = "john@example.com",
            Password = "password",
            Category = "private",
            PhoneNumber = "1234567890",
            BirthDate = new DateTime(1980, 1, 1)
        },
        new Contact
        {
            Name = "Jane",
            LastName = "Smith",
            Email = "jane@example.com",
            Password = "password",
            Category = "private",
            PhoneNumber = "0987654321",
            BirthDate = new DateTime(1985, 5, 10)
        },
        new Contact
        {
            Name = "Alice",
            LastName = "Johnson",
            Email = "alice@example.com",
            Password = "password",
            Category = "private",
            PhoneNumber = "5555555555",
            BirthDate = new DateTime(1975, 12, 25)
        },
        new Contact
        {
            Name = "Bob",
            LastName = "Williams",
            Email = "bob@example.com",
            Password = "password",
            Category = "private",
            PhoneNumber = "9876543210",
            BirthDate = new DateTime(1990, 8, 15)
        },
        new Contact
        {
            Name = "Emily",
            LastName = "Brown",
            Email = "emily@example.com",
            Password = "password",
            Category = "business",
            SubCategory = "boss",
            PhoneNumber = "1231231234",
            BirthDate = new DateTime(1988, 4, 30)
        },
        new Contact
        {
            Name = "Michael",
            LastName = "Jones",
            Email = "michael@example.com",
            Password = "password",
            Category = "other",
            SubCategory = "Client",
            PhoneNumber = "7778889999",
            BirthDate = new DateTime(1972, 6, 20)
        },
        new Contact
        {
            Name = "Sarah",
            LastName = "Davis",
            Email = "sarah@example.com",
            Password = "password",
            Category = "private",
            PhoneNumber = "1112223333",
            BirthDate = new DateTime(1983, 9, 5)
        },
        new Contact
        {
            Name = "David",
            LastName = "Martinez",
            Email = "david@example.com",
            Password = "password",
            Category = "private",
            PhoneNumber = "2223334444",
            BirthDate = new DateTime(1978, 11, 12)
        },
        new Contact
        {
            Name = "Emma",
            LastName = "Garcia",
            Email = "emma@example.com",
            Password = "password",
            Category = "private",
            PhoneNumber = "5554443333",
            BirthDate = new DateTime(1995, 3, 8)
        },
        new Contact
        {
            Name = "Christopher",
            LastName = "Rodriguez",
            Email = "chris@example.com",
            Password = "password",
            Category = "private",
            PhoneNumber = "6667778888",
            BirthDate = new DateTime(1981, 7, 17)
        }
            };

            return contacts;
        }


        public static async Task SeedRoles(IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope()) // create scope 
            {
                /*var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roleNames = { UserRoles.User, UserRoles.Admin};

                foreach (var roleName in roleNames)
                {
                    var roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }*/

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
                   // await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
