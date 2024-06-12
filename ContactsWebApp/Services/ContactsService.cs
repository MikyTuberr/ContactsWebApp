using ContactsWebApp.DTO;

namespace ContactsWebApp.Services
{
    public class ContactsService : IContactsService
    {
        public bool IsContactsModelValid(ContactDto contactDto)
        {
            if (contactDto.Name == null ||
               contactDto.LastName == null ||
               contactDto.Email == null ||
               contactDto.Password == null ||
               contactDto.Category == null ||
               contactDto.PhoneNumber == null)
            {
                return false;
            }

            // Check if birthdate is valid (older than 13 years old)
            if (DateTime.Now.Subtract(contactDto.BirthDate).TotalDays < 13 * 365)
            {
                return false;
            }

            // Check if subcategory and category are valid
            if (contactDto.Category == "business" && string.IsNullOrEmpty(contactDto.SubCategory))
            {
                return false;
            }

            return true;
        }
    }
}
