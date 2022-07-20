using System.ComponentModel.DataAnnotations;

namespace MartEdu.Service.DTOs.Authors
{
    public class AuthorForCreationDto
    {
        [Required, MinLength(2), MaxLength(32)]
        public string Name { get; set; }

        [Required, MinLength(4), MaxLength(32), EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(64)]
        public string Password { get; set; }
    }
}
