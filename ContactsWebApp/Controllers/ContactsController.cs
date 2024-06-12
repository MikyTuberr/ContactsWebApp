using ContactsWebApp.DTO;
using ContactsWebApp.Models;
using ContactsWebApp.Repositories;
using ContactsWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsRepository _contactsRepository; // injected contacts repository, which does all db operations 
        private readonly IContactsService _contactsService; // injected contacts service, does all non db operations
        private readonly IAuthService _authService; //injected auth service, for password and email validation
        private readonly IAuthRepository _authRepository; // injected auth repository, for unique email validation

        public ContactsController(
            IContactsRepository contactsRepository, 
            IContactsService contactsService,
            IAuthService authService,
            IAuthRepository authRepository)
        {
            _contactsRepository = contactsRepository;
            _contactsService = contactsService;
            _authService = authService;
            _authRepository = authRepository;
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
        public async Task<ActionResult<Contact>> AddContact(ContactDto model)
        {
            // Validates the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); } 

            // Ensure that required fields are not null and valid
            if (!_contactsService.IsContactsModelValid(model)) { return BadRequest("Some data is null."); }

             // Check if email is valid
            if (!_authService.IsValidEmail(model.Email)) { return BadRequest("Email must be a valid email address."); };

            // Check if password must has at least 16 characters long and contain a mix of uppercase letters, lowercase letters, numbers, and special characters.
            if (!_authService.IsPasswordStrong(model.Password))
            { return BadRequest("Password must be at least 16 characters long and contain a mix of uppercase letters, lowercase letters, numbers, and special characters."); };

            // Check if user with the same email already exists
            if (await _contactsRepository.IsEmailUsedAsync(model.Email)) { return BadRequest("Email is already used."); }

            var contact = new Contact // create new contact entity, based on data from contact dto
            {
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Category = model.Category,
                SubCategory = model.SubCategory,
                PhoneNumber = model.PhoneNumber,
                BirthDate = model.BirthDate
            };

            var result = await _contactsRepository.AddAsync(contact); // add new contact

            if (result == null)
            {
                return NotFound("Unable to add the contact.");
            }

            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact); // return 201 and added contact with it's id
        }

        // Put : api/contacts/id
        [HttpPut("{id}")]
        public async Task<ActionResult<Contact>> EditContact(int id, ContactDto model)
        {
            // Validates the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Ensure that required fields are not null and valid
            if (!_contactsService.IsContactsModelValid(model)) { return BadRequest("Some data is null."); }

            // Check if email is valid
            if (!_authService.IsValidEmail(model.Email)) { return BadRequest("Email must be a valid email address."); };

            // Check if password must has at least 16 characters long and contain a mix of uppercase letters, lowercase letters, numbers, and special characters.
            if (!_authService.IsPasswordStrong(model.Password))
            { return BadRequest("Password must be at least 16 characters long and contain a mix of uppercase letters, lowercase letters, numbers, and special characters."); };

            var contact = new Contact // create new contact entity, based on data from contact dto
            {
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Category = model.Category,
                SubCategory = model.SubCategory,
                PhoneNumber = model.PhoneNumber,
                BirthDate = model.BirthDate
            };

            var result = await _contactsRepository.EditAsync(id, contact); // edit contact by id

            if (result == null)
            {
                return NotFound("Unable to edit contact.");
            }
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact); // return 201 and edited contact with it's id
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
