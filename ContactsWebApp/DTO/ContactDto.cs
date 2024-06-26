﻿namespace ContactsWebApp.DTO
{
    public class ContactDto
    {

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string? SubCategory { get; set; }
        public string PhoneNumber { get; set; } = null!;

        public DateTime BirthDate { get; set; }
    }
    
}
