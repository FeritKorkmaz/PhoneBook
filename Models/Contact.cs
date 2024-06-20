using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
    }   
}