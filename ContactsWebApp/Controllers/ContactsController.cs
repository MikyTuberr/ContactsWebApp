using ContactsWebApp.DTO;
using ContactsWebApp.Models;
using ContactsWebApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContactsWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository _contactsRepository; // injected contacts repository, which does all db operations 

        public ContactsController(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }

        // Get : api/contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contacts = await _contactsRepository.GetAllAsync(); // get contacts
            if (contacts == null)
            {
                return NotFound("Unable to find contacts.");
            }
            return contacts;
        }

        // Get : api/contacts/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _contactsRepository.GetAsync(id); // get contact by id
            if (contact == null)
            {
                return NotFound("Unable to find contact.");
            }
            return contact;
        }

        // Post : api/contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> AddContact(ContactDto contactDto)
        {
            if (!ModelState.IsValid) // validation of contact dto
            {
                return BadRequest(ModelState);
            }

            var contact = new Contact // create new contact entity, based on data from contact dto
            {
                Name = contactDto.Name,
                LastName = contactDto.LastName,
                Email = contactDto.Email,
                Password = contactDto.Password,
                Category = contactDto.Category,
                SubCategory = contactDto.SubCategory,
                PhoneNumber = contactDto.PhoneNumber,
                BirthDate = contactDto.BirthDate
            };

            var result = await _contactsRepository.AddAsync(contact); // add new contact

            if (result == null)
            {
                return NotFound("Unable to add the contact.");
            }

            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact); // return 201 and added contact with it's id
        }

        // Put : api/contacts/id
        [HttpPut]
        public async Task<ActionResult<Contact>> EditContact(int id, ContactDto contactDto)
        {
            if (!ModelState.IsValid) // validation of contact dto
            {
                return BadRequest(ModelState);
            }

            var contact = new Contact // create new contact entity, based on data from contact dto
            {
                Name = contactDto.Name,
                LastName = contactDto.LastName,
                Email = contactDto.Email,
                Password = contactDto.Password,
                Category = contactDto.Category,
                SubCategory = contactDto.SubCategory,
                PhoneNumber = contactDto.PhoneNumber,
                BirthDate = contactDto.BirthDate
            };

            var result = await _contactsRepository.EditAsync(id, contact); // edit contact by id

            if (result == null)
            {
                return NotFound("Unable to edit contact.");
            }
            return NoContent(); // return no content 204
        }

        // Delete : api/contacts/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            var result = await _contactsRepository.DeleteAsync(id); // delete contact by id
            if (result == null)
            {
                return NotFound("Unable to delete contact.");
            }
            return NoContent(); // return no content 204
        }
    }
}
