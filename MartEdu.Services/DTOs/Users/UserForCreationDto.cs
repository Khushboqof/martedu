using System.ComponentModel.DataAnnotations;


namespace MartEdu.Service.DTOs.Users
{
    public class UserForCreationDto
    {
        [Required, MinLength(2), MaxLength(32)]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(32)]
        public string LastName { get; set; }

        [Required, MinLength(4), MaxLength(32)]
        public string Username { get; set; }

        [Required, EmailAddress, MaxLength(32)]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(64)]
        public string Password { get; set; }
    }
}
