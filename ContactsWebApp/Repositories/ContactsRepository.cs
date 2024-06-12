using ContactsWebApp.Data;
using ContactsWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsWebApp.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly AppDbContext _context; // injected db context, provides communication with db

        public ContactsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailUsedAsync(string email)
        {
            if (_context.Contacts == null) // check if "Contacts" table isn't empty
            {
                return false;
            }

            // Check if the email exists in the Contacts table
            var existingContactWithEmail = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Email == email);

            // If email is already used by another contact, return true
            if (existingContactWithEmail != null)
            {
                return true;
            }

            // If no entity uses the email, return false
            return false;
        }

        public async Task<int?> AddAsync(Contact contact)
        {
            if (_context.Contacts == null) // check if "Contacts" table isn't empty
            {
                return null;
            }
            _context.Contacts.Add(contact); // add contact
            return await SaveChangesAsync();
        }

        public async Task<int?> EditAsync(int id, Contact updated)
        {
            if (_context.Contacts == null) // check if "Contacts" table isn't empty
            {
                return null;
            }

            var existing = await GetAsync(id); // get contact by id

            if (existing == null) // check if it exists
            {
                return null; 
            }

            // update values
            existing.Name = updated.Name;
            existing.LastName = updated.LastName;
            existing.Email = updated.Email;
            existing.Password = updated.Password;
            existing.Category = updated.Category;
            existing.SubCategory = updated.SubCategory;
            existing.PhoneNumber = updated.PhoneNumber;
            existing.BirthDate = updated.BirthDate;

            _context.Entry(existing).State = EntityState.Modified; // change state to modified
            return await SaveChangesAsync();
        }

        public async Task<List<Contact>?> GetAllAsync()
        {
            if (_context.Contacts == null) // check if "Contacts" table isn't empty
            {
                return null;
            }
            return await _context.Contacts.ToListAsync(); // return list of contacts
        }

        public async Task<Contact?> GetAsync(int id)
        {
            if (_context.Contacts == null) // check if "Contacts" table isn't empty
            {
                return null;
            }
            var contact = await _context.Contacts.FindAsync(id); // find contact by id
            if(contact == null)
            {
                return null;
            }
            return contact;
        }

        public async Task<int?> DeleteAsync(int id)
        {
            if (_context.Contacts == null) // check if "Contacts" table isn't empty
            {
                return null;
            }

            var contact = await GetAsync(id); // get contact by id

            if (contact == null)
            {
                return null;
            }

            _context.Contacts.Remove(contact); // remove contact
            return await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
