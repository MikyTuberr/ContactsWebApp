﻿using ContactsWebApp.DTO;

namespace ContactsWebApp.Services
{
    public interface IContactsService
    {
        public bool IsContactsModelValid(ContactDto contactDto); // check if contacts model is valid
    }
}
