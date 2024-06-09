using ContactsWebApp.Models;

namespace ContactsWebApp.Repositories
{
    public interface IContactsRepository 
    {
        public Task<List<Contact>?> GetAllAsync(); // get all contacts collection
        public Task<Contact?> GetAsync(int id); // get contact by id

        public Task<int?> AddAsync(Contact contact); // add contact
        public Task<int?> EditAsync(int id, Contact coontact); // edit contact by id

        public Task<int?> DeleteAsync(int id); // delete contact by id
        public Task<int> SaveChangesAsync(); // save changes to db
    }
}
