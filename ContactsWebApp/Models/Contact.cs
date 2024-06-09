namespace ContactsWebApp.Models
{
    public class Contact
    {
        public int Id { get; set; }

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
